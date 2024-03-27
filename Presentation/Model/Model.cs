using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic;

namespace Presentation.Model
{
    public class Model
    {
        private LogicAbstract _logic;

        public Model(Action playerUpdateCallback, Action<bool> reactiveElementsUpdateCallback)
        {
            _logic = LogicAbstract.CreateInstance(playerUpdateCallback, reactiveElementsUpdateCallback, null);
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

        public ObservableCollection<Data.Player> GetPlayers()
        {
            return _logic.GetObservableCollection();
        }
    }
}
