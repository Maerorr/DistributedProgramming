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

    public enum MoveDirection
    {
        Up,
        Down,
        Left,
        Right
    }

    public interface ILogic
    {
        public IData data { get; }

        public static ILogic Create(Action UpdatePlayersCallback, IData? data = null)
        {
            IData dataApi = data ?? IData.Create();
            dataApi.PlayersChanged += UpdatePlayersCallback;
            return new Logic(dataApi);
        }

        public List<ILogicPlayer> GetPlayers();
        public Guid AddPlayer();
        public void MovePlayer(Guid playerId, MoveDirection direction);
    }
}
