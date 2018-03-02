using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace FormsApp11
{
    public class MainViewModel
    {
        private readonly INavigation navigation;
        public ICommand NavigateCommand { get; }

        public MainViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            this.NavigateCommand = new Command(async () => await this.Navigate());
        }

        private async Task Navigate()
        {
            await this.navigation.PushAsync(new PersonListPage());
        }
    }
}
