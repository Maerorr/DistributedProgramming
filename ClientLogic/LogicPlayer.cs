using ClientData;

namespace ClientLogic
{
    internal class LogicPlayer : ILogicPlayer
    {
        public string Name { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Speed { get; set; }

        public LogicPlayer(string name, float x, float y, float speed)
        {
            Name = name;
            X = x;
            Y = y;
            Speed = speed;
        }

        public LogicPlayer(IPlayer player)
        {
            Name = player.Name;
            X = player.X;
            Y = player.Y;
            Speed = player.Speed;
        }
    }
}
