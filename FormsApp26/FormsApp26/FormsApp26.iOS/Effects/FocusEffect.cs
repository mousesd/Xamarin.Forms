using System;
using System.ComponentModel;
using System.Diagnostics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

using FormsApp26.iOS.Effects;

[assembly:ResolutionGroupName("HomeNet")]
[assembly:ExportEffect(typeof(FocusEffect), "FocusEffect")]
namespace FormsApp26.iOS.Effects
{
    public class FocusEffect : PlatformEffect
    {
        private UIColor backgroundColor;

        protected override void OnAttached()
        {
            try
            {
                this.Control.BackgroundColor = backgroundColor = UIColor.FromRGB(204, 153, 255);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Can't set property on attached control. Message: {ex.Message}");
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
                this.Control.BackgroundColor = Equals(this.Control.BackgroundColor, backgroundColor)
                    ? UIColor.White
                    : backgroundColor;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Can't set property on attached control. Message: {ex.Message}");
            }
        }
    }
}
