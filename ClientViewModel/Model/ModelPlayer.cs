using ClientLogic;

namespace ClientViewModel.Model
{
    internal class ModelPlayer : IModelPlayer
    {
        public string Name { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Speed { get; set; }

        public ModelPlayer(string name, float x, float y, float speed)
        {
            Name = name;
            X = x;
            Y = y;
            Speed = speed;
        }

        public ModelPlayer(ILogicPlayer player)
        {
            Name = player.Name;
            X = player.X;
            Y = player.Y;
            Speed = player.Speed;
        }
    }
}
