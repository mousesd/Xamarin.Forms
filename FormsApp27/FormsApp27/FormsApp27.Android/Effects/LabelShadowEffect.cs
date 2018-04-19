using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FormsApp27.Droid.Effects;
using FormsApp27.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly:ResolutionGroupName("HomeNet")]
[assembly:ExportEffect(typeof(LabelShadowEffect), "LabelShadowEffect")]
namespace FormsApp27.Droid.Effects
{
    public class LabelShadowEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            var effect = (ShadowEffect)this.Element.Effects.FirstOrDefault(e => e is ShadowEffect);
            if (effect == null)
                return;

            if (!(this.Control is TextView textView))
                return;

            textView.SetShadowLayer(effect.Radius, effect.DistanceX, effect.DistanceY, effect.Color.ToAndroid());
        }

        protected override void OnDetached() { }
    }
}
