using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using ObjCRuntime;
using UIKit;

namespace FormsApp17.iOS
{
    public static class BarButtonItemExtensions
    {
        private enum AssociationPolicy
        {
            Assign = 0,
            RetainNonatomic = 1,
            CopyNonatomic = 3,
            Retain = 01401,
            Copy = 01403
        }

        private static readonly NSString BadgeKey = new NSString(@"BadgeKey");

        [DllImport(Constants.ObjectiveCLibrary)]
        private static extern IntPtr objc_setAssociatedObject(IntPtr obj, IntPtr key, IntPtr value
            , AssociationPolicy policy);

        [DllImport(Constants.ObjectiveCLibrary)]
        private static extern IntPtr objc_getAssociatedObject(IntPtr obj, IntPtr key);

        private static CAShapeLayer GetBadgeLayer(UIBarButtonItem barButtonItem)
        {
            var handle = objc_getAssociatedObject(barButtonItem.Handle, BadgeKey.Handle);
            if (handle != IntPtr.Zero)
            {
                var value = ObjCRuntime.Runtime.GetNSObject(handle);
                if (value != null)
                    return value as CAShapeLayer;
            }
            return null;
        }

        private static void DrawRoundedRect(CAShapeLayer layer, CGRect rect, float radius, UIColor color, bool filled)
        {
            layer.FillColor = filled ? color.CGColor : UIColor.White.CGColor;
            layer.StrokeColor = color.CGColor;
            layer.Path = UIBezierPath.FromRoundedRect(rect, radius).CGPath;
        }

        public static void AddBadge(this UIBarButtonItem barButtonItem, string text, UIColor backgroundColor
            , UIColor textColor, bool filled = true, float fontSize = 11.0f)
        {
            if (string.IsNullOrEmpty(text))
                return;

            var offset = CGPoint.Empty;
            if (backgroundColor == null)
                backgroundColor = UIColor.Red;

            var font = UIFont.SystemFontOfSize(fontSize);
            if (UIDevice.CurrentDevice.CheckSystemVersion(9, 0))
                font = UIFont.MonospacedDigitSystemFontOfSize(fontSize, UIFontWeight.Regular);

            if (!(barButtonItem.ValueForKey(new NSString(@"view")) is UIView view))
                return;

            var badgeLayer = GetBadgeLayer(barButtonItem);
            badgeLayer?.RemoveFromSuperLayer();

            var badgeSize = text.StringSize(font);
            var width = badgeSize.Width + 2;
            var height = badgeSize.Height;
            if (width < height)
                width = height;

            var x = view.Frame.Width - width + offset.X;
            var badgeFrame = new CGRect(new CGPoint(x, offset.Y), new CGSize(width, height));
            badgeLayer = new CAShapeLayer();
            DrawRoundedRect(badgeLayer, badgeFrame, 7.0f, backgroundColor, filled);
            view.Layer.AddSublayer(badgeLayer);

            var label = new CATextLayer
            {
                String = text,
                TextAlignmentMode = CATextLayerAlignmentMode.Center,
                FontSize = font.PointSize,
                Frame = badgeFrame,
                ForegroundColor = filled ? textColor.CGColor : UIColor.White.CGColor,
                BackgroundColor = backgroundColor.CGColor,
                ContentsScale = UIScreen.MainScreen.Scale
            };
            label.SetFont(CGFont.CreateWithFontName(font.Name));
            badgeLayer.AddSublayer(label);
            objc_setAssociatedObject(barButtonItem.Handle, BadgeKey.Handle, badgeLayer.Handle
                , AssociationPolicy.RetainNonatomic);
        }

        public static void UpdateBadge(this UIBarButtonItem barButtonItem, string text, UIColor backgroundColor
            , UIColor textColor)
        {
            var badgeLayer = GetBadgeLayer(barButtonItem);
            if (string.IsNullOrEmpty(text) || text == "0")
            {
                badgeLayer?.RemoveFromSuperLayer();
                objc_setAssociatedObject(barButtonItem.Handle, BadgeKey.Handle, new CAShapeLayer().Handle
                    , AssociationPolicy.Assign);
                return;
            }

            if (badgeLayer?.Sublayers?.First(l => l is CATextLayer) is CATextLayer textLayer)
                textLayer.String = text;
            else
                barButtonItem.AddBadge(text, backgroundColor, textColor);
        }
    }
}
