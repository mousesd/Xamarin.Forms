using System;
using System.Diagnostics;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

using FormsApp26.UWP.Effects;

[assembly:ResolutionGroupName("HomeNet")]
[assembly:ExportEffect(typeof(FocusEffect), "FocusEffect")]
namespace FormsApp26.UWP.Effects
{
    public class FocusEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                ((Control)this.Control).Background = new SolidColorBrush(Windows.UI.Colors.Cyan);
                ((FormsTextBox)this.Control).BackgroundFocusBrush = new SolidColorBrush(Windows.UI.Colors.White);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Can't set property on attached control. Message: {ex.Message}");
            }
        }

        protected override void OnDetached() { }
    }
}
