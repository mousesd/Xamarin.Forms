using System;
using CoreGraphics;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

using FormsApp24.iOS.Effects;

[assembly: ResolutionGroupName("Xamarin")]
[assembly: ExportEffect(typeof(LabelShadowEffect), "LabelShadowEffect")]
namespace FormsApp24.iOS.Effects
{
    public class LabelShadowEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            this.Control.Layer.CornerRadius = 5;
            this.Control.Layer.ShadowColor = Color.Black.ToCGColor();
            this.Control.Layer.ShadowOffset = new CGSize(5, 5);
            this.Control.Layer.ShadowOpacity = 1;
        }

        protected override void OnDetached()
        {
            throw new NotImplementedException();
        }
    }
}
