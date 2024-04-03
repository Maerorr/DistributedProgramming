using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientPresentation.Model
{
    public class ModelConnectionHandler
    {
        public event Action<string> log;
        public event Action<string> onMessage;
        public event Action onClose;
        public event Action onError;

        internal ILogicConnectionHandler connectionHandler { get; private set; }

        public ModelConnectionHandler(ILogicConnectionHandler connectionHandler)
        {
            this.connectionHandler = connectionHandler;
            this.connectionHandler.log += (message) => log?.Invoke(message);
            this.connectionHandler.onMessage += (message) => onMessage?.Invoke(message);
            this.connectionHandler.onClose += () => onClose?.Invoke();
            this.connectionHandler.onError += () => onError?.Invoke();
        }

        public async Task Connect(Uri peer)
        {
            await connectionHandler.Connect(peer);
        }

        public async Task Disconnect()
        {
            await connectionHandler.Disconnect();
        }

        public bool IsConnected()
        {
            return connectionHandler.IsConnected();
        }
    }
}
