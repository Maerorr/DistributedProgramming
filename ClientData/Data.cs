using GlobalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace ClientData
{
    internal class Data : IData
    {
        public IConnectionService ConnectionService { get; set; }

        public event Action PlayersChanged;
        private List<IPlayer> players;

        public Data(IConnectionService? connectionService)
        {
            this.ConnectionService = connectionService ?? new ConnectionService();
        }

        public void Add(IPlayer player)
        {
            players.Add(player);
        }

        public List<IPlayer> GetPlayers()
        {
            return new List<IPlayer>(players);
        }

        public void Remove(string name)
        {
            players.Remove(players.Where(i => i.Name == name).Single());
        }

        public async Task MovePlayer(MoveDirection direction)
        {
            if (ConnectionService.IsConnected())
            {
                MovePlayerCommand cmd = new MovePlayerCommand(new Guid(), 70, 70);
                await ConnectionService.SendAsync(Serializer.Serialize(cmd));
            }
        }
    }
}
