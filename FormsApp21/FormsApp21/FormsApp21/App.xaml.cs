﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace FormsApp21
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

            this.MainPage = new NavigationPage(new LoginPage());
		}
	}
}