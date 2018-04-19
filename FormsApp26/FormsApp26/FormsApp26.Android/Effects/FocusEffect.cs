using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

using FormsApp26.Droid.Effects;

[assembly: ResolutionGroupName("HomeNet")]
[assembly: ExportEffect(typeof(FocusEffect), "FocusEffect")]
namespace FormsApp26.Droid.Effects
{
    public class FocusEffect : PlatformEffect
    {
        Android.Graphics.Color backgroundColor;

        protected override void OnAttached()
        {
            try
            {
                backgroundColor = Android.Graphics.Color.LightGreen;
                this.Control.SetBackgroundColor(backgroundColor);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Can't set property on attached control. Message: {ex.Message}");
            }
        }

        protected override void OnDetached() { }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(args);

            if (args.PropertyName != "IsFocused")
                return;

            try
            {
                this.Control.SetBackgroundColor(
                    ((Android.Graphics.Drawables.ColorDrawable)this.Control.Background).Color == backgroundColor
                        ? Android.Graphics.Color.White
                        : backgroundColor);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Can't set property on attached control. Message: {ex.Message}");
            }
        }
    }
}
