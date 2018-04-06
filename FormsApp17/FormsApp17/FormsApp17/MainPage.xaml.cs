using System.Linq;
using FormsApp17.Services;
using Xamarin.Forms;

namespace FormsApp17
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

	    private void OnValueChanged(object sender, ValueChangedEventArgs e)
	    {
            if (this.ToolbarItems.Count > 0)
                DependencyService.Get<IToolbarItemBadgeService>()
                    .SetBadge(this, ToolbarItems.First(), $"{e.NewValue}", Color.Red, Color.Wheat);
        }
	}
}
