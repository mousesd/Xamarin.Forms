using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FormsApp21.Services;
using FormsApp21.Validations;
using FormsApp21.Views;
using MvvmHelpers;
using Xamarin.Forms;

namespace FormsApp21.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly INavigation navigation;
        public ValidatableObject<string> Email { get; set; }
        public ValidatableObject<string> Password { get; set; }

        public ICommand LoginCommand { get; }

        public LoginViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            this.Email = new ValidatableObject<string>(this.PropertyChangedCallback, "mousesd@gmail.com"
                , new EmailValidator());
            this.Password = new ValidatableObject<string>(this.PropertyChangedCallback, "gmlakd"
                , new PasswordValidator());

            this.LoginCommand = new Command(async () => await this.Login()
                , () => this.Email.IsValid && this.Password.IsValid);
        }

        private void PropertyChangedCallback()
        {
            ((Command)this.LoginCommand).ChangeCanExecute();
        }

        private async Task Login()
        {
            this.IsBusy = true;
            this.PropertyChangedCallback();

            string token = await DependencyService.Get<IFirebaseAuthenticator>()
                .Login(this.Email.Value, this.Password.Value);
            await navigation.PushAsync(new HomePage(token));

            this.IsBusy = false;
            this.PropertyChangedCallback();
        }
    }
}
