using ServerData;

namespace ServerLogic
{
    internal class Logic : ILogic
    {
        public IData data { get; }
        static List<string> playerNames = new List<string>();
        static Random rnd = new Random();

        private List<Guid> botGuids = new List<Guid>();

        public Logic(IData data)
        {
            this.data = data;

            playerNames.Add("John");
            playerNames.Add("Bob");
            playerNames.Add("Alice");
            playerNames.Add("Eve");
            playerNames.Add("Matthew");
            playerNames.Add("Adam");
            playerNames.Add("Janice");

            for (int i = 0; i < 3; i++)
            {
                botGuids.Add(AddPlayer());
            }

            MoveRandomBot();
        }

        public List<ILogicPlayer> GetPlayers()
        {
            return data.GetPlayers()
                .Select(player => new LogicPlayer(player))
                .Cast<ILogicPlayer>()
                .ToList();
        }

        public void MovePlayer(Guid playerId, MoveDirection direction)
        {
            if (data.HasPlayer(playerId))
            {
                data.MovePlayer(playerId, (ServerData.MoveDirection)direction);
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public Guid AddPlayer()
        {
            int nameIdx = rnd.Next(playerNames.Count);
            float x = (float)rnd.NextDouble() * 200.0f;
            float y = (float)rnd.NextDouble() * 200.0f;
            float speed = 20.0f;
            return data.AddPlayer(playerNames[nameIdx], x, y, speed);
        }

        public async void MoveRandomBot()
        {
            while (true)
            {
                await Task.Delay(1000);

                int botIdx = rnd.Next(botGuids.Count);
                MoveDirection direction = (MoveDirection)rnd.Next(4);
               data.MovePlayer(botGuids[botIdx], (ServerData.MoveDirection)direction);
            }
        }
    }
}
