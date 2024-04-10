using ClientData;
using GlobalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientLogic
{
    internal class LogicConnectionService : ILogicConnectionService
    {
        public event Action<string>? Log;
        public event Action? OnConnectionStateChanged;

        public event Action<string>? OnMessage;
        public event Action? OnError;
        public event Action? OnDisconnect;

        internal ClientData.IConnectionService connection { get; private set; }

        public LogicConnectionService(ClientData.IConnectionService connectionService)
        {
            connection = connectionService;

            connectionService.Log += (message) => Log?.Invoke(message);
            connectionService.OnConnectionStateChanged += () => OnConnectionStateChanged?.Invoke();
            connectionService.OnMessage+= (message) => OnMessage?.Invoke(message);
            connectionService.OnError += () => OnError?.Invoke();
        }

        public async Task Connect(Uri peer)
        {
            await connection.Connect(peer);
        }

        public async Task Disconnect()
        {
            if (connection != null)
            {
                await connection.Disconnect();
            }
        }

        public bool IsConnected()
        {
            if (connection != null)
            {
                connection.IsConnected();
            }
            return false;
        }
    }
}
