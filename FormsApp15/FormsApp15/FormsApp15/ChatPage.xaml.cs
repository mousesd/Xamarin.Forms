using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormsApp15.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FormsApp15
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ChatPage : ContentPage
	{
		public ChatPage()
		{
			InitializeComponent();
		}

	    public ChatPage(string userName) : this()
	    {
	        this.BindingContext = new ChatViewModel(userName);
	    }
	}
}