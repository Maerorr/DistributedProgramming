using ClientData;
using System.Reflection;

namespace ClientLogic
{
    internal class Logic : ILogic, IObserver<List<IPlayer>>
    {
        public IData data { get; }
        public ILogicConnectionService ConnectionService { get; }
        public Action updateCallback;

        private List<ILogicPlayer> cachedPlayers;
        private IDisposable DataSubscriptionHandle;

        public Logic(Action playerUpdateCallback, IData data)
        {
            this.data = data;
            this.updateCallback = playerUpdateCallback;
            data.Subscribe(this);

            if (data.ConnectionService != null)
            {
                ConnectionService = new LogicConnectionService(data.ConnectionService);
                ConnectionService.OnConnectionStateChanged += OnConnectionStateChanged;
                ConnectionService.OnDisconnect += OnConnectionStateChanged;
                ConnectionService.OnError += OnConnectionStateChanged;
                ConnectionService.Log += Log;

                Task.Run(() => ConnectionService.Connect(new Uri(@"ws://localhost:13337")));
            }
        }

        public List<ILogicPlayer> GetPlayers()
        {
            return cachedPlayers;
        }

        public async Task MovePlayer(MoveDirection moveDirection)
        {
            await data.MovePlayer((ClientData.MoveDirection)moveDirection);
        }

        private void OnConnectionStateChanged()
        {
            bool actual = data.ConnectionService.IsConnected();

            if (!actual)
            {
                Task.Run(() => ConnectionService.Connect(new Uri(@"ws://localhost:13337")));
            } else
            {
                data.RequestUpdate();
            }
        }

        private void Log(string text)
        {
            Console.WriteLine(text);
        }

        public void OnCompleted()
        {
            DataSubscriptionHandle?.Dispose();
        }

        public void OnError(Exception error)
        {
            
        }

        public void OnNext(List<IPlayer> value)
        {
            cachedPlayers = value
                .Select(player => new LogicPlayer(player))
                .Cast<ILogicPlayer>()
                .ToList();
            updateCallback?.Invoke();
        }
    }
}
