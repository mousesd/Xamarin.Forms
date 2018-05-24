using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormsApp31.ViewModels;
using Xamarin.Forms;

namespace FormsApp31
{
	public partial class MainPage : ContentPage
	{
	    private readonly MainViewModel viewModel;

		public MainPage()
		{
			InitializeComponent();
            this.BindingContext = viewModel = new MainViewModel(this.Navigation);
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await viewModel.GetTodoItems();
        }
    }
}
