using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FormsApp15.Web
{
    public class EasyChatService
    {
        private readonly RequestDelegate requestDelegate;
        private readonly ConcurrentDictionary<string, WebSocket> sockets 
            = new ConcurrentDictionary<string, WebSocket>();

        public EasyChatService(RequestDelegate requestDelegate)
        {
            this.requestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.WebSockets.IsWebSocketRequest)
            {
                await this.requestDelegate.Invoke(context);
                return;
            }

            var token = context.RequestAborted;
            var socket = await context.WebSockets.AcceptWebSocketAsync();
            var guid = Guid.NewGuid().ToString();
            sockets.TryAdd(guid, socket);

            while (true)
            {
                if (token.IsCancellationRequested)
                    break;

                var message = await GetMessageAsync(socket, token);
                Debug.WriteLine($"Received message: {message} at {DateTime.Now:yyyy-MM-dd HH:mm:ss}");

                if (string.IsNullOrEmpty(message))
                {
                    if (socket.State != WebSocketState.Open)
                        break;
                    continue;
                }

                foreach (var s in sockets.Where(o => o.Value.State == WebSocketState.Open))
                    await SendMessageAsync(s.Value, message, token);
            }

            sockets.TryRemove(guid, out WebSocket redundantSocket);
            await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Session ended", token);
            socket.Dispose();
        }

        private async Task<string> GetMessageAsync(WebSocket socket, CancellationToken token)
        {
            WebSocketReceiveResult result;
            var buffer = new ArraySegment<byte>(new byte[4096]);
            string message;

            do
            {
                token.ThrowIfCancellationRequested();

                result = await socket.ReceiveAsync(buffer, token);
                var bytes = buffer.Skip(buffer.Offset).Take(result.Count).ToArray();
                message = Encoding.UTF8.GetString(bytes);
            } while (!result.EndOfMessage);

            return result.MessageType != WebSocketMessageType.Text ? null : message;
        }

        private Task SendMessageAsync(WebSocket socket, string message, CancellationToken token)
        {
            var bytes = Encoding.UTF8.GetBytes(message);
            var buffer = new ArraySegment<byte>(bytes);

            return socket.SendAsync(buffer, WebSocketMessageType.Text, true, token);
        }
    }
}