using Xamarin.Forms;
using FormsApp13.Resources;

namespace FormsApp13
{
	public class MainCodePage : ContentPage
	{
		public MainCodePage ()
		{
		    Content = new StackLayout
		    {
		        Children =
		        {
		            new Label { Text = AppResources.NotesLabel },
		            new Entry { Placeholder = AppResources.NotesPlaceholder },
		            new Button { Text = AppResources.AddButton }
		        }
		    };

            this.Padding = new Thickness(0, 25, 0, 0);
		}
	}
}
