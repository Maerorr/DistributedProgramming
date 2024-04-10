using ServerData;

namespace ServerLogic
{
    internal class Logic : ILogic
    {
        public IData data { get; }

        public Logic(IData data)
        {
            this.data = data;
        }

        public List<ILogicPlayer> GetPlayers()
        {
            return data.GetPlayers()
                .Select(player => new LogicPlayer(player))
                .Cast<ILogicPlayer>()
                .ToList();
        }

        public void MovePlayer(Guid playerId, float x, float y)
        {
            if (data.HasPlayer(playerId))
            {
                data.MovePlayer(playerId, x, y);
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }
    }
}
