using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FormsApp29.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MasterPage : ContentPage
	{
	    public ListView ListView
	    {
            get { return this.listView; }
	    }

		public MasterPage ()
		{
			InitializeComponent();
		}
	}
}