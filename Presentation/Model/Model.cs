using ClientPresentation.Model;
using Logic;

namespace Presentation.Model
{
    public class Model
    {
        private LogicAbstract _logic;
        public Action onPlayersUpdated;
        public ModelConnectionHandler connectionHandler { get; private set; }

        public Model(Action playerUpdateCallback, Action<bool> reactiveElementsUpdateCallback)
        {
            _logic = LogicAbstract.CreateInstance(playerUpdateCallback, reactiveElementsUpdateCallback, null);
            connectionHandler = new ModelConnectionHandler(_logic.GetConnectionHandler());
            onPlayersUpdated += _logic.OnPlayersUpdated;
        }

        public void AddPlayer()
        {
            _logic.AddPlayer("test");
        }
        
        public void RemovePlayer()
        {
            _logic.RemovePlayer("test");
        }

        public void MoveUp()
        {
            _logic.MovePlayer("up");
        }

        public void MoveDown()
        {
            _logic.MovePlayer("down");
        }

        public void MoveLeft()
        {
            _logic.MovePlayer("left");
        }

        public void MoveRight()
        {
            _logic.MovePlayer("right");
        }

        public void RequestUpdate()
        {
            _logic.RequestUpdate();
        }

        public List<ModelPlayer> GetPlayers()
        {
            return _logic.GetPlayers()
                .Select(player => new ModelPlayer(player))
                .ToList();
        }
    }
}
