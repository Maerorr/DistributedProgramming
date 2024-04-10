using ClientLogic;

namespace ClientViewModel.Model
{
    public interface IModelPlayer
    {
        public string Name { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Speed { get; set; }

        public static IModelPlayer Create(string name, float x, float y, float speed)
        {
            return new ModelPlayer(name, x, y, speed);
        }
    }

    public interface IModel
    {
        public ILogic logic { get; }

        public static IModel Create(ILogic? logic = null)
        {
            ILogic logicApi = logic ?? ILogic.Create();
            return new Model(logicApi);
        }

        public List<IModelPlayer> GetPlayers();
        public void MoveUp();
        public void MoveDown();
        public void MoveLeft();
        public void MoveRight();
    }
}
