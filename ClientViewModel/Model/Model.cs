using ClientLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientViewModel.Model
{
    internal class Model : IModel
    {
        public ILogic logic {  get; }
        public Action playerUpdateCallback;

        public Model(ILogic logic)
        {
            this.logic = logic;
        }

        public List<IModelPlayer> GetPlayers()
        {
            return logic
                .GetPlayers()
                .Select(player => new ModelPlayer(player))
                .Cast<IModelPlayer>()
                .ToList();
        }

        public void MoveUp()
        {
            Task.Run(async () => await logic.MovePlayer(MoveDirection.Up));
        }

        public void MoveDown()
        {
            Task.Run(async () => await logic.MovePlayer(MoveDirection.Down));
        }

        public void MoveLeft()
        {
            Task.Run(async () => await logic.MovePlayer(MoveDirection.Left));
        }

        public void MoveRight()
        {
            Task.Run(async () => await logic.MovePlayer(MoveDirection.Right));
        }
    }
}
