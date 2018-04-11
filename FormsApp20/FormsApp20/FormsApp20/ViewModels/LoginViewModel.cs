using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

using FormsApp20.Validations;

namespace FormsApp20.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private Action PropertyChangedAction
        {
            get { return ((Command)LoginCommand).ChangeCanExecute; }
        }

        public ValidatableObject<string> Email { get; }
        public ValidatableObject<string> Password { get; }
        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            this.LoginCommand = new Command(async () => await this.LoginHandler()
                , () => this.Email.IsValid && this.Password.IsValid);

            this.Email = new ValidatableObject<string>(this.PropertyChangedAction
                , new EmailValidator()) { Value = "myuser@service.com" };
            this.Password = new ValidatableObject<string>(this.PropertyChangedAction
                , new PasswordValidator()) { Value = "Qwerty123" };
        }

        private async Task LoginHandler()
        {
            this.IsBusy = true;
            this.PropertyChangedAction();
            await Task.Run(() => { Thread.Sleep(3000); });
            this.IsBusy = false;
            this.PropertyChangedAction();
        }
    }
}
