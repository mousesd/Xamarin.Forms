using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace FormsApp18
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

		    var bootstrapper = new AppBootstrapper();
		    this.MainPage = bootstrapper.CreateMainPage();
		}
	}
}
