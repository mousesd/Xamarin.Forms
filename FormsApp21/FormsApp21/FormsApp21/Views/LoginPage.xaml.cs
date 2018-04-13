using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormsApp21.ViewModels;
using Xamarin.Forms;

namespace FormsApp21
{
	public partial class LoginPage : ContentPage
	{
		public LoginPage()
		{
			InitializeComponent();
            this.BindingContext = new LoginViewModel(this.Navigation);
		}
	}
}
