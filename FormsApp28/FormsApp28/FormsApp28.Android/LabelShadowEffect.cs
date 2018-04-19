using System.ComponentModel;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

using FormsApp28.Droid;
using FormsApp28.Effects;

[assembly:ResolutionGroupName("HomeNet")]
[assembly:ExportEffect(typeof(LabelShadowEffect), "LabelShadowEffect")]
namespace FormsApp28.Droid
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

            if (!(this.Control is TextView textView))
                return;

            float radius = ShadowEffect.GetRadius(this.Element);
            Android.Graphics.Color color = ShadowEffect.GetColor(this.Element).ToAndroid();
            float dx = ShadowEffect.GetDistanceX(this.Element);
            float dy = ShadowEffect.GetDistanceY(this.Element);

            textView.SetShadowLayer(radius, dx, dy, color);
        }
    }
}