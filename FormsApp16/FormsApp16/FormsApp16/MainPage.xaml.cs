using System.Diagnostics.CodeAnalysis;
using Xamarin.Forms;
using FormsApp16.ViewModels;

namespace FormsApp16
{
	[SuppressMessage("ReSharper", "RedundantExtendsListEntry")]
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		    this.BindingContext = new MainViewModel();
		}
	}
}
