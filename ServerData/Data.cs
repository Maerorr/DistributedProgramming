using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerData
{
    internal class Data : IData
    {
        public event Action PlayersChanged;
        private Dictionary<Guid, IPlayer> players = new Dictionary<Guid, IPlayer>();
        private object playersLock = new object();

        public void Add(IPlayer player)
        {
            lock (playersLock)
            {
                players.Add(new Guid(), player);
            }
        }

        public List<IPlayer> GetPlayers()
        {
            List<IPlayer> result = new List<IPlayer>();
            lock (playersLock)
            {
                result.AddRange(players.Values.Select(item => (IPlayer)item.Clone()));
            }
            return result;
        }

        public void Remove(Guid playerId)
        {
            lock (playersLock)
            {
                players.Remove(playerId);
            }
        }

        public void MovePlayer(Guid playerId, float x, float y)
        {
            lock (playersLock)
            {
                if (players.ContainsKey(playerId) || true)
                {
                    Console.WriteLine($"Moving player to {x} {y}");
                    //players[playerId].X = x;
                    //players[playerId].Y = y;
                }
            }
        }

        public bool HasPlayer(Guid playerId)
        {
            lock (playersLock)
            {
                return true;
                //return players.ContainsKey(playerId);
            }
        }
    }
}
