using GlobalApi;

namespace ClientData
{
    internal class Data : IData
    {
        public IConnectionService ConnectionService { get; set; }

        public event Action PlayersChanged;
        private List<IPlayer> players;
        private Guid ourPlayerId;

        public Data(IConnectionService? connectionService)
        {
            this.ConnectionService = connectionService ?? new ConnectionService();
            ConnectionService.OnMessage += OnMessage;
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
                MovePlayerCommand cmd = new MovePlayerCommand(ourPlayerId, (GlobalApi.MoveDirection)direction);
                await ConnectionService.SendAsync(Serializer.Serialize(cmd));
            }
        }

        public void OnMessage(string message)
        {
            if (ConnectionService == null) return;

            string header = Serializer.GetHeader(message);
            if (header == null) return;

            if (header == JoinResponse.HEADER)
            {
                JoinResponse response = Serializer.Deserialize<JoinResponse>(message);
                ourPlayerId = response.guidForPlayer;

                GetPlayersCommand cmd = new GetPlayersCommand();
                ConnectionService.SendAsync(Serializer.Serialize(cmd));
            }

            if (header == UpdatePlayersResponse.HEADER)
            {
                UpdatePlayersResponse response = Serializer.Deserialize<UpdatePlayersResponse>(message);
                players = new List<IPlayer>();
                foreach (PlayerData p in response.players)
                {
                    players.Add(new Player(p.name, p.x, p.y, p.speed));
                }
                PlayersChanged.Invoke();
            }
        }
    }
}
