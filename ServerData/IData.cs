using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerData
{
    public interface IPlayer : ICloneable
    {
        public string Name { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Speed { get; set; }

        public static IPlayer Create(string name,  float x, float y, float speed)
        {
            return new Player(name, x, y, speed);
        }
    }

    public interface IData
    {
        public abstract List<IPlayer> GetPlayers();
        public event Action PlayersChanged;

        public static IData Create()
        {
            return new Data();
        }

        public void MovePlayer(Guid playerId, float x, float y);
        public bool HasPlayer(Guid playerId);
    }
}
