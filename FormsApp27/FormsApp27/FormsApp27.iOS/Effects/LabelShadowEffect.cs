using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreGraphics;
using FormsApp27.Effects;
using FormsApp27.iOS.Effects;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly:ResolutionGroupName("HomeNet")]
[assembly:ExportEffect(typeof(LabelShadowEffect), "LabelShadowEffect")]
namespace FormsApp27.iOS.Effects
{
    public class LabelShadowEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            var effect = (ShadowEffect)this.Element.Effects.FirstOrDefault(e => e is ShadowEffect);
            if (effect == null)
                return;

            this.Control.Layer.CornerRadius = effect.Radius;
            this.Control.Layer.ShadowColor = effect.Color.ToCGColor();
            this.Control.Layer.ShadowOffset = new CGSize(effect.DistanceX, effect.DistanceY);
            this.Control.Layer.ShadowOpacity = 1;
        }

        protected override void OnDetached() { }
    }
}
