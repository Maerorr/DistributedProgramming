using Data;
using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientLogic
{
    internal class LogicConnectionHandler : ILogicConnectionHandler
    {
        public event Action<string> log;
        public event Action<string> onMessage;
        public event Action onClose;
        public event Action onError;

        internal IConnectionHandler dataConnectionHandler { get; private set; }

        public LogicConnectionHandler(IConnectionHandler dataConnectionHandler)
        {
            this.dataConnectionHandler = dataConnectionHandler;

            dataConnectionHandler.log += (message) => log?.Invoke(message);
            dataConnectionHandler.onMessage += (message) => onMessage?.Invoke(message);
            dataConnectionHandler.onClose += () => onClose?.Invoke();
            dataConnectionHandler.onError += () => onError?.Invoke();
        }

        public async Task Connect(Uri peer)
        {
            await dataConnectionHandler.Connect(peer);
        }

        public async Task Disconnect()
        {
            await dataConnectionHandler.Disconnect();
        }

        public bool IsConnected()
        {
            return dataConnectionHandler.IsConnected();
        }
    }
}
