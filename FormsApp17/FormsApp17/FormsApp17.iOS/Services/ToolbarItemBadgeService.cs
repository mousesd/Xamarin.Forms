using System;
using System.Collections.Generic;
using System.Text;
using FormsApp17.iOS.Services;
using FormsApp17.Services;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: Dependency(typeof(ToolbarItemBadgeService))]
namespace FormsApp17.iOS.Services
{
    public class ToolbarItemBadgeService : IToolbarItemBadgeService
    {
        public void SetBadge(Page page, ToolbarItem item, string value, Color backgroundColor, Color textColor)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var renderer = Platform.GetRenderer(page);
                if (renderer == null)
                {
                    renderer = Platform.CreateRenderer(page);
                    Platform.SetRenderer(page, renderer);
                }

                var viewController = renderer.ViewController;
                var rightBarButtonItems = viewController?.ParentViewController?.NavigationItem?.RightBarButtonItems;
                int index = page.ToolbarItems.IndexOf(item);
                if (rightBarButtonItems != null && rightBarButtonItems.Length > index)
                {
                    var barItem = rightBarButtonItems[index];
                    if (barItem != null)
                        barItem.UpdateBadge(value, backgroundColor.ToUIColor(), textColor.ToUIColor());
                }
            });
        }
    }
}
