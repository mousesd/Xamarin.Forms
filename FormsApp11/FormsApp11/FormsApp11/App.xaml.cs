using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace FormsApp11
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

		    MainPage = new NavigationPage(new MainPage());
		}
	}
}
