using System.ComponentModel;
using CoreGraphics;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

using FormsApp28.Effects;
using FormsApp28.iOS.Effects;

[assembly:ResolutionGroupName("HomeNet")]
[assembly:ExportEffect(typeof(LabelShadowEffect), "LabelShadowEffect")]
namespace FormsApp28.iOS.Effects
{
    public class LabelShadowEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            this.UpdateControl();
        }

        protected override void OnDetached() { }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            if (args.PropertyName == ShadowEffect.RadiusProperty.PropertyName
                || args.PropertyName == ShadowEffect.ColorProperty.PropertyName
                || args.PropertyName == ShadowEffect.DistanceXProperty.PropertyName
                || args.PropertyName == ShadowEffect.DistanceYProperty.PropertyName)
            {
                this.UpdateControl();
            }
        }

        private void UpdateControl()
        {
            if (this.Control == null || this.Element == null)
                return;

            this.Control.Layer.CornerRadius = ShadowEffect.GetRadius(this.Element);
            this.Control.Layer.ShadowColor = ShadowEffect.GetColor(this.Element).ToCGColor();
            this.Control.Layer.ShadowOffset = new CGSize(
                ShadowEffect.GetDistanceX(this.Element), ShadowEffect.GetDistanceY(this.Element));
            this.Control.Layer.ShadowOpacity = 1.0F;
        }
    }
}
