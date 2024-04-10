using ClientViewModel.Model;

namespace ClientViewModel.ViewModel
{
    public class ViewModelPlayer : IModelPlayer
    {
        public string Name { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Speed { get; set; }

        public ViewModelPlayer(string name, float x, float y, float speed)
        {
            Name = name;
            X = x;
            Y = y;
            Speed = speed;
        }

        public ViewModelPlayer(IModelPlayer player)
        {
            Name = player.Name;
            X = player.X;
            Y = player.Y;
            Speed = player.Speed;
        }
    }
}
