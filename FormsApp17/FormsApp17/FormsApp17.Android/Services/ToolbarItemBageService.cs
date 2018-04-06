using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Plugin.CurrentActivity;

using FormsApp17.Services;
using FormsApp17.Droid.Services;

[assembly: Dependency(typeof(ToolbarItemBadgeService))]
namespace FormsApp17.Droid.Services
{
    public class ToolbarItemBadgeService : IToolbarItemBadgeService
    {
        public void SetBadge(Page page, ToolbarItem item, string value, Color backgroundColor, Color textColor)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (!(CrossCurrentActivity.Current.Activity.FindViewById(Resource.Id.toolbar) is
                    Android.Support.V7.Widget.Toolbar toolbar))
                    return;

                if (string.IsNullOrEmpty(value))
                    return;

                int index = page.ToolbarItems.IndexOf(item);
                if (toolbar.Menu.Size() > index)
                {
                    var menuItem = toolbar.Menu.GetItem(index);
                    BadgeDrawable.SetBadgeText(CrossCurrentActivity.Current.Activity
                        , menuItem, value, backgroundColor.ToAndroid(), textColor.ToAndroid());
                }
            });
        }
    }
}
