using System.Windows.Input;
using Xamarin.Forms;

namespace FormsApp19.ViewModels
{
    public class LoginViewModel
    {
        public ICommand OnLoginSuccessCommand { get; }
        public ICommand OnLoginErrorCommand { get; }
        public ICommand OnLoginCancelCommand { get; }

        public LoginViewModel()
        {
            this.OnLoginSuccessCommand =
                new Command<string>(p => this.DisplayAlert("Success", $"Authentication succeed: {p}"));
            this.OnLoginErrorCommand =
                new Command<string>(p => this.DisplayAlert("Error", $"Authentication failed: {p}"));
            this.OnLoginCancelCommand =
                new Command(p => this.DisplayAlert("Cancel", "Authentication cancelled by the user"));
        }

        private void DisplayAlert(string title, string message)
        {
            if (!(Application.Current is App app))
                return;

            app.MainPage.DisplayAlert(title, message, "OK");
        }
    }
}
