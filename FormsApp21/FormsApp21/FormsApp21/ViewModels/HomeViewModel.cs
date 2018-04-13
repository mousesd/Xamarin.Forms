using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmHelpers;
using Xamarin.Forms;

namespace FormsApp21.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private readonly string token;

        private string remoteData;
        public string RemoteData
        {
            get { return remoteData; }
            private set { this.SetProperty(ref remoteData, value); }
        }

        public ICommand RequestCommand { get; }

        public HomeViewModel(string token)
        {
            this.token = token;
            this.RequestCommand = new Command<bool>(async authorized => await this.RequestHandler(authorized)
                , _ => !this.IsBusy);
        }

        private void PropertyChangedCallback()
        {
            ((Command)this.RequestCommand).ChangeCanExecute();
        }

        private async Task RequestHandler(bool authorized)
        {
            this.IsBusy = true;
            this.PropertyChangedCallback();

            this.RemoteData = string.Empty;
            using (var httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(3) })
            {
                if (authorized)
                {
                    var authValue = new AuthenticationHeaderValue("Bearer", token);
                    httpClient.DefaultRequestHeaders.Authorization = authValue;
                }

                string urlString = @"http://172.26.104.117";
                var response = await httpClient.GetAsync($"{urlString}:5001/api/data");
                string content = await response.Content.ReadAsStringAsync();

                this.RemoteData = $"StatusCode: {response.StatusCode}, Content: {content}";
            }

            this.IsBusy = false;
            this.PropertyChangedCallback();
        }
    }
}
