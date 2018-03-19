using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmHelpers;
using Xamarin.Forms;

namespace FormsApp15.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        #region == Fields & Properties ==
        private readonly INavigation navigation;

        private string userName;
        public string UserName
        {
            get { return userName; }
            set { this.SetProperty(ref userName, value); }
        }
        #endregion

        #region == Commands ==
        public ICommand SignupCommand { get; set; }
        #endregion

        #region == Constructors ==
        public MainViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            this.SignupCommand = new Command(async () => await this.OnSignup());
        } 
        #endregion

        private async Task OnSignup()
        {
            await this.navigation.PushAsync(new ChatPage(this.UserName));
        }
    }
}
