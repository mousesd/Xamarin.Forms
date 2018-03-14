using Xamarin.Forms;
using Plugin.Multilingual;
using FormsApp14.Resources;

namespace FormsApp14
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

		    var ci = CrossMultilingual.Current.DeviceCultureInfo;
            AppResources.Culture = ci;

			MainPage = new FormsApp14.MainPage();
		}
	}
}
