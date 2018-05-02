using System;
using Xamarin.Forms;
using FormsApp29.Models;

namespace FormsApp29
{
    public partial class MainPage : MasterDetailPage
	{
		public MainPage()
		{
            InitializeComponent();

            masterPage.ListView.ItemSelected += this.OnItemSelected;
		}

	    private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
	    {
            if (!(e.SelectedItem is MasterPageItem item))
                return;

	        this.Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
            this.IsPresented = false;
            masterPage.ListView.SelectedItem = null;
	    }
	}
}
