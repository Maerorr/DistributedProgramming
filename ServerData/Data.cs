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

        public Guid AddPlayer(string name, float x, float y, float speed)
        {
            Guid newGuid = Guid.NewGuid();
            lock (playersLock)
            {
                players.Add(newGuid, IPlayer.Create(name, x, y, speed));
            }
            PlayersChanged.Invoke();
            return newGuid;
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

        public void MovePlayer(Guid playerId, MoveDirection direction)
        {
            lock (playersLock)
            {
                if (players.ContainsKey(playerId))
                {
                    IPlayer player = players[playerId];
                    if (direction == MoveDirection.Up)
                    {
                        player.Y -= player.Speed;
                    }
                    else if (direction == MoveDirection.Down)
                    {
                        player.Y += player.Speed;
                    }
                    else if (direction == MoveDirection.Left)
                    {
                        player.X -= player.Speed;
                    }
                    else if (direction == MoveDirection.Right)
                    {
                        player.X += player.Speed;
                    }
                    //Console.WriteLine($"Moving player {player.Name} to {(int)player.X} {(int)player.Y}");
                    PlayersChanged.Invoke();
                }
            }
        }

        public bool HasPlayer(Guid playerId)
        {
            lock (playersLock)
            {
                return players.ContainsKey(playerId);
            }
        }
    }
}
