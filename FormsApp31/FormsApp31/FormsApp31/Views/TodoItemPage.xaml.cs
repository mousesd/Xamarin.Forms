using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormsApp31.Models;
using FormsApp31.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FormsApp31.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TodoItemPage : ContentPage
	{
		public TodoItemPage(TodoItem todoItem, bool isNew = false)
		{
			InitializeComponent();
            this.BindingContext = new TodoItemViewModel(this.Navigation, todoItem, isNew);
		}
	}
}
