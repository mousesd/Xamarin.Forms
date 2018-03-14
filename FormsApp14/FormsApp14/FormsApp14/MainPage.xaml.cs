using Xamarin.Forms;
using FormsApp14.ViewModels;

namespace FormsApp14
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		    this.BindingContext = new MainViewModel();
		}
	}
}
