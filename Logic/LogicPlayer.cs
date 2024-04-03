using Data;
using Logic;

namespace ClientLogic
{
    internal class LogicPlayer : ILogicPlayer
    {
        public float Diameter { get; }
        public float X { get; }
        public float Y { get; }

        public string Name { get; }

        public ILogicVector2 Position { get; }

        public LogicPlayer(IPlayer player)
        {
            this.Name = player.Name;
            this.Position = new LogicVector2(player.Position);
            this.Diameter = player.Diameter;
            this.X = player.X;
            this.Y = player.Y;
        }
    }
}
