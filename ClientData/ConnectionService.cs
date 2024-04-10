using GlobalApi;

namespace ClientData
{
    internal class ConnectionService : IConnectionService
    {
        public event Action<string>? Log;
        public event Action? OnConnectionStateChanged;

        public event Action<string>? OnMessage;
        public event Action? OnError;
        public event Action? OnDisconnect;

        internal WebSocketConnection connection { get; private set; }

        public async Task Connect(Uri peer)
        {
            try
            {
                connection = await WebSocketClient.Connect(peer, Log);
                connection.OnMessage = (message) => OnMessage?.Invoke(message);
                connection.OnError = () => OnError?.Invoke();
                connection.OnClose = () => OnDisconnect?.Invoke();
            }
            catch (Exception e)
            {
                Log?.Invoke(e.Message);
                OnError?.Invoke();
            }
        }

        public async Task Disconnect()
        {
            if (connection != null)
            {
                await connection.DisconnectAsync();
            }
        }

        public bool IsConnected()
        {
            return connection != null;
        }

        public async Task SendAsync(string message)
        {
            if (connection != null)
            {
                await connection.SendAsync(message);
            }
        }
    }
}