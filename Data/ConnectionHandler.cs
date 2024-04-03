using Data;
using GlobalAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientData
{
    internal class ConnectionHandler : IConnectionHandler
    {
        public event Action<string> log;
        public event Action<string> onMessage;
        public event Action onClose;
        public event Action onError;

        internal WebSocketConnection wsConnection { get; private set; }

        public async Task Connect(Uri peer)
        {
           try
           {
                wsConnection = await WebSocketClient.Connect(peer, log);
                wsConnection.onMessage = (message) => onMessage?.Invoke(message);
                wsConnection.onClose = () => onClose?.Invoke();
                wsConnection.onError = () => onError?.Invoke();
           }
           catch (Exception e)
           {
                log?.Invoke(e.Message);
                onError?.Invoke();
           }
        }

        public async Task Disconnect()
        {
            if (wsConnection != null)
            {
                await wsConnection.DisconnectAsync();
            }
        }

        public bool IsConnected()
        {
            return wsConnection != null;
        }

        public async Task SendAsync(string message)
        {
            if (wsConnection != null)
            {
                await wsConnection.SendAsync(message);
            }
        }
    }
}
