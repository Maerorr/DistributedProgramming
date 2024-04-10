using GlobalApi;
using System.Net.WebSockets;
using System.Text;

namespace ClientData
{
    internal class WebSocketClient
    {
        public static async Task<WebSocketConnection> Connect(Uri peer, Action<string> log)
        {
            ClientWebSocket webSocket = new ClientWebSocket();
            await webSocket.ConnectAsync(peer, CancellationToken.None);
            switch (webSocket.State)
            {
                case WebSocketState.Open:
                    log.Invoke($"Opening WebSocket connection to remote server {peer}");
                    WebSocketConnection socket = new ClientWebSocketConnection(webSocket, peer, log);
                    return socket;
                default:
                    log.Invoke($"Cannot connect to remote node status {webSocket.State}");
                    throw new WebSocketException($"Cannot connect to remote node status {webSocket.State}");
            }
        }

        private class ClientWebSocketConnection : WebSocketConnection
        {
            private readonly ClientWebSocket webSocket;
            private readonly Action<string> log;
            private readonly Uri peer;

            public ClientWebSocketConnection(ClientWebSocket webSocket, Uri peer, Action<string> log)
            {
                this.webSocket = webSocket;
                this.peer = peer;
                this.log = log;
                Task.Run(ClientMessageLoop);
            }

            public override Task DisconnectAsync()
            {
                return webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing websocket", CancellationToken.None);
            }

            protected override Task SendTask(string message)
            {
                return webSocket.SendAsync(message.GetArraySegment(), WebSocketMessageType.Text, true, CancellationToken.None);
            }

            public override string ToString()
            {
                return peer.ToString();
            }

            private void ClientMessageLoop()
            {
                try
                {
                    byte[] buffer = new byte[1024];
                    while (true)
                    {
                        ArraySegment<byte> segment = new ArraySegment<byte>(buffer);
                        WebSocketReceiveResult result = webSocket.ReceiveAsync(segment, CancellationToken.None).Result;
                        if (result.MessageType == WebSocketMessageType.Close)
                        {
                            OnClose?.Invoke();
                            webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "I am closing", CancellationToken.None).Wait();
                            return;
                        }
                        int count = result.Count;
                        while (!result.EndOfMessage)
                        {
                            if (count >= buffer.Length)
                            {
                                OnClose?.Invoke();
                                webSocket.CloseAsync(WebSocketCloseStatus.InvalidPayloadData, "That's too long", CancellationToken.None).Wait();
                                return;
                            }
                            segment = new ArraySegment<byte>(buffer, count, buffer.Length - count);
                            result = webSocket.ReceiveAsync(segment, CancellationToken.None).Result;
                            count += result.Count;
                        }
                        string _message = Encoding.UTF8.GetString(buffer, 0, count);
                        OnMessage?.Invoke(_message);
                    }
                }
                catch (Exception _ex)
                {
                    log($"Connection has been broken because of an exception {_ex}");
                    webSocket.CloseAsync(WebSocketCloseStatus.InternalServerError, "Connection has been broken because of an exception", CancellationToken.None).Wait();
                }
            }
        }
    }
}
