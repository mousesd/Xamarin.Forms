using System;
using Android.Util;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

using FormsApp24.Droid.Effects;

[assembly:ResolutionGroupName("Xamarin")]
[assembly:ExportEffect(typeof(LabelShadowEffect), "LabelShadowEffect")]
namespace FormsApp24.Droid.Effects
{
    public class LabelShadowEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            if (!(this.Control is TextView textView))
                return;

            textView.SetShadowLayer(5, 5, 5, Color.Black.ToAndroid());
        }

        protected override void OnDetached()
        {
            
        }
    }
}
