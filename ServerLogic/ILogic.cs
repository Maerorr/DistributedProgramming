using ServerData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ServerLogic
{
    public interface ILogicPlayer
    {
        public string Name { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Speed { get; set; }

        public static ILogicPlayer Create(string name, float x, float y, float speed)
        {
            return new LogicPlayer(name, x, y, speed);
        }
    }

    public interface ILogic
    {
        public IData data { get; }

        public static ILogic Create(IData? data = null)
        {
            IData dataApi = data ?? IData.Create();
            return new Logic(dataApi);
        }

        public List<ILogicPlayer> GetPlayers();
        public void MovePlayer(Guid playerId, float x, float y);
    }
}
