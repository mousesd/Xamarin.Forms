using Xamarin.Forms;

using FormsApp30.Views;
using MenuItem = FormsApp30.Models.MenuItem;

namespace FormsApp30.Services
{
    public class NavigationService
    {
        public static void Navigate(MenuItem menuItem)
        {
            // ReSharper disable once AccessToStaticMemberViaDerivedType
            if (!(App.Current.MainPage is MasterDetailPage masterDetailPage))
                return;

            switch (menuItem.Id)
            {
                case 1:
                    masterDetailPage.Detail = new NavigationPage(new ContactsPage());
                    break;
                case 2:
                    masterDetailPage.Detail = new NavigationPage(new TodoItemPage());
                    break;
            }

            if (masterDetailPage.IsPresented)
                masterDetailPage.IsPresented = false;
        }
    }
}
