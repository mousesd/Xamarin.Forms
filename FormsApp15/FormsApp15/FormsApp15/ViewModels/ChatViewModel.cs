using System;
using System.Diagnostics;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using MvvmHelpers;
using Newtonsoft.Json;
using FormsApp15.Models;
using Plugin.DeviceInfo;

namespace FormsApp15.ViewModels
{
    public class ChatViewModel : BaseViewModel
    {
        #region == Fields & Properties ==
        private readonly string userName;
        private readonly ClientWebSocket client;
        private readonly CancellationTokenSource cts;

        private ObservableRangeCollection<Message> messages;
        public ObservableRangeCollection<Message> Messages
        {
            get { return this.messages; }
            set { this.SetProperty(ref this.messages, value); }
        }

        private string text;
        public string Text
        {
            get { return this.text; }
            set { this.SetProperty(ref this.text, value); }
        }

        public bool IsConnected
        {
            get
            {
                if (this.client == null)
                    return false;

                return this.client.State == WebSocketState.Open;
            }
        }
        #endregion

        #region == Commands ==
        public Command SendCommand { get; set; }
        #endregion

        #region == Constructors ==
        public ChatViewModel(string userName)
        {
            this.userName = userName;
            this.client = new ClientWebSocket();
            this.cts = new CancellationTokenSource();

            this.Messages = new ObservableRangeCollection<Message>();
            this.SendCommand = new Command(async () => await this.OnSend());

            this.ConnectServerAsync();
        }
        #endregion

        private async Task OnSend()
        {
            var messge = new Message
            {
                Name = this.userName,
                DateTime = DateTime.Now,
                Text = this.Text,
                UserId = CrossDeviceInfo.Current.Id
            };

            var bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(messge));
            var buffer = new ArraySegment<byte>(bytes);

            await this.client.SendAsync(buffer, WebSocketMessageType.Text, true, cts.Token);
            this.Text = string.Empty;
        }

        private async void ConnectServerAsync()
        {
            //# TODO: 각 플랫폼별 서버접속 설정 처리
            //if (Device.RuntimePlatform == Device.iOS)
            //    await this.client.ConnectAsync(new Uri("ws://localhost:5000"), cts.Token);
            //else
            //    await this.client.ConnectAsync(new Uri("ws://10.0.2.2.:5000"), cts.Token);

            //# NOTE: IIS Express를 아래 IP를 이용해 접속히 불가능함
            //# - ASP.NET Core를 이용해 WebSocket 서버를 개발하는 방법 확인??
            //# - IIS Express를 통해서 호스팅 되는 서비스가 localhost가 아닌 특정 IP로 서비스가 가능한 방법을 찾을 필요 있음
            await this.client.ConnectAsync(new Uri("ws://172.26.104.117:5000"), cts.Token);

            this.SendCommand.ChangeCanExecute();
            Debug.WriteLine($"WebSocket State: {this.client.State}");

            await Task.Run(async () =>
            {
                while (true)
                {
                    WebSocketReceiveResult result;
                    var buffer = new ArraySegment<byte>(new byte[4096]);
                    do
                    {
                        result = await this.client.ReceiveAsync(buffer, cts.Token);
                        var bytes = buffer.Skip(buffer.Offset).Take(result.Count).ToArray();
                        string jsonString = Encoding.UTF8.GetString(bytes);

                        try
                        {
                            var message = JsonConvert.DeserializeObject<Message>(jsonString);
                            this.Messages.Add(message);
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine($"Message: {ex.Message}");
                        }
                    } while (!result.EndOfMessage);
                }
            });
        }
    }
}
