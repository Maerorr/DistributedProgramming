using ClientData;

namespace ClientLogic
{
    internal class Logic : ILogic
    {
        public IData data { get; }
        public ILogicConnectionService ConnectionService { get; }

        public Logic(IData data)
        {
            this.data = data;
            ConnectionService = new LogicConnectionService(data.ConnectionService);
            Task.Run(() => ConnectionService.Connect(new Uri(@"ws://localhost:13337")));
        }

        public List<ILogicPlayer> GetPlayers()
        {
            return data.GetPlayers()
                .Select(player => new LogicPlayer(player))
                .Cast<ILogicPlayer>()
                .ToList();
        }

        public async Task MovePlayer(MoveDirection moveDirection)
        {
            await data.MovePlayer((ClientData.MoveDirection)moveDirection);
        }
    }
}
