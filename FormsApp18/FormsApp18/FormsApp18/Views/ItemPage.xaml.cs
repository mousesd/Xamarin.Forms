using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormsApp18.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FormsApp18.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ItemPage : ContentPageBase<ItemsViewModel>
	{
		public ItemPage ()
		{
			InitializeComponent ();
		}
	}
}