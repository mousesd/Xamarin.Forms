using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormsApp20.ViewModels;
using Xamarin.Forms;

namespace FormsApp20
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
