using GlobalApi;

namespace ClientData
{
    internal class Data : IData
    {
        public IConnectionService ConnectionService { get; set; }

        private HashSet<IObserver<List<IPlayer>>> observers = new HashSet<IObserver<List<IPlayer>>>();

        private List<IPlayer> players;
        private Guid ourPlayerId;

        public Data(IConnectionService? connectionService)
        {
            this.ConnectionService = connectionService ?? new ConnectionService();
            ConnectionService.OnMessage += OnMessage;
        }

        ~Data()
        {
            List<IObserver<List<IPlayer>>> cachedObservers = observers.ToList();
            foreach (IObserver<List<IPlayer>>? observer in cachedObservers)
            {
                observer?.OnCompleted();
            }
        }

        public List<IPlayer> GetPlayers()
        {
            return new List<IPlayer>(players);
        }

        public void Remove(string name)
        {
            players.Remove(players.Where(i => i.Name == name).Single());
        }

        public async Task MovePlayer(MoveDirection direction)
        {
            if (ConnectionService.IsConnected())
            {
                MovePlayerCommand cmd = new MovePlayerCommand {
                    Header = Headers.MovePlayerCommand,
                    PlayerId = ourPlayerId,
                    Direction = (GlobalApi.MoveDirection)direction
                };
                await ConnectionService.SendAsync(Serializer.Serialize(cmd));
            }
        }

        public void OnMessage(string message)
        {
            if (ConnectionService == null) return;

            string header = Serializer.GetHeader(message);
            if (header == null) return;

            if (header == Headers.JoinResponse)
            {
                JoinResponse response = Serializer.Deserialize<JoinResponse>(message);
                ourPlayerId = response.GuidForPlayer;

                RequestUpdate();
            }

            if (header == Headers.UpdatePlayersResponse)
            {
                UpdatePlayersResponse response = Serializer.Deserialize<UpdatePlayersResponse>(message);
                players = new List<IPlayer>();
                foreach (PlayerData p in response.Players)
                {
                    players.Add(new Player(p.Name, p.X, p.Y, p.Speed));
                }
                foreach (IObserver<List<IPlayer>>? observer in observers)
                {
                    observer.OnNext(new List<IPlayer>(players));
                }
            }

            if (header == Headers.MovePlayerResponse)
            {
                MovePlayerResponse response = Serializer.Deserialize<MovePlayerResponse>(message);
            }
        }

        public void RequestUpdate()
        {
            if (ConnectionService == null) return;
            GetPlayersCommand cmd = new GetPlayersCommand();
            ConnectionService.SendAsync(Serializer.Serialize(cmd));
        }

        public IDisposable Subscribe(IObserver<List<IPlayer>> observer)
        {
            observers.Add(observer);
            return new DataDisposable(this, observer);
        }

        private void UnSubscribe(IObserver<List<IPlayer>> observer)
        {
            observers.Remove(observer);
        }

        private class DataDisposable : IDisposable
        {
            private readonly Data data;
            private readonly IObserver<List<IPlayer>> observer;

            public DataDisposable(Data data, IObserver<List<IPlayer>> observer)
            {
                this.data = data;
                this.observer = observer;
            }

            public void Dispose()
            {
                data.UnSubscribe(observer);
            }
        }
    }
}
