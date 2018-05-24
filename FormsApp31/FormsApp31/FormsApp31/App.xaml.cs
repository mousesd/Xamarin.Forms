using System;
using FormsApp31.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace FormsApp31
{
	public partial class App : Application
	{
        public static readonly TodoItemManager TodoItemManager = new TodoItemManager(new TodoService());

        public App()
		{
			InitializeComponent();

		    MainPage = new NavigationPage(new MainPage());
		}
	}
}
