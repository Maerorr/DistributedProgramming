
using ServerData;


namespace ServerLogic
{
    internal class Logic : LogicAbstract
    {
        public Logic(DataStorageAbstract dataStorage) : base(dataStorage) { }

        public override List<ILogicPlayer> GetPlayers()
        {
            List<ILogicPlayer> players = new List<ILogicPlayer>();
            foreach (IServerPlayer player in dataStorage.GetAll())
            {
                players.Add(new LogicPlayer(player));
            }
            return players;
        }

        public override void MovePlayer(string name, float x, float y)
        {
            IServerPlayer serverPlayer = dataStorage.GetAll().First(p => p.Name == name);
            serverPlayer.Move(x, y);
        }
    }
}
