using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormsApp21.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FormsApp21.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage
	{
	    private readonly string token;

	    public HomePage()
	    {
	        InitializeComponent();
	    }

	    public HomePage(string token) : this()
	    {
	        this.token = token;
	        this.BindingContext = new HomeViewModel(token);
	    }
	}
}
