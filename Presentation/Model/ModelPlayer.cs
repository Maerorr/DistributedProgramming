using ClientLogic;
using Logic;

namespace ClientPresentation.Model
{
    public class ModelPlayer
    {
        public float Diameter { get; }
        public float X { get; }
        public float Y { get; }

        public string Name { get; }

        public ModelVector2 Position { get; }

        public ModelPlayer(ILogicPlayer player)
        {
            this.Diameter = player.Diameter;
            this.X = player.X;
            this.Y = player.Y;
            this.Name = player.Name;
            this.Position = new ModelVector2(player.Position);
        }
    }
}
