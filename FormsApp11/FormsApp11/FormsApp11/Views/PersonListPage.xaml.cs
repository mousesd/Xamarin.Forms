using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FormsApp11
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PersonListPage : ContentPage
	{
		public PersonListPage ()
		{
			InitializeComponent();
		    this.BindingContext = new PersonListViewModel();
		}
	}
}
