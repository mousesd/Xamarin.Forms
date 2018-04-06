using Xamarin.Forms;

namespace FormsApp17.Services
{
    public interface IToolbarItemBadgeService
    {
        void SetBadge(Page page, ToolbarItem item, string value, Color backgroundColor, Color textColor);
    }
}
