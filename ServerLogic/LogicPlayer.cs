
using ServerData;

namespace ServerLogic
{
    internal class LogicPlayer : ILogicPlayer
    {
        public float Diameter { get; }
        public float X { get; private set; }
        public float Y { get; private set; }

        public string Name { get; }

        public ILogicVector2 Position { get; set; }


        public LogicPlayer(IServerPlayer player)
        {
            this.Name = player.Name;
            this.Position = new LogicVector2(player.Position);
            this.Diameter = player.Diameter;
            this.X = player.X;
            this.Y = player.Y;
        }

        public void Move(ILogicVector2 position)
        {
            this.Position.X = position.X;
            this.Position.Y = position.Y;
            this.X += position.X;
            this.Y += position.Y;
        }
    }
}
