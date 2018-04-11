using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormsApp19.ViewModels;
using Xamarin.Forms;

namespace FormsApp19
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();

		    this.BindingContext = new LoginViewModel();
		}
	}
}
