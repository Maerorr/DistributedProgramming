using GlobalApi;
using ServerLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ServerPresentation
{
    internal class Program
    {
        private readonly ILogic logic;
        private WebSocketConnection? connection;

        private Program()
        {
            this.logic = ILogic.Create(UpdatePlayers);
        }

        private async Task StartConnection()
        {
            while (true)
            {
                Console.WriteLine("Waiting for connection...");
                await WebSocketServer.StartServer(13337, OnConnect);
            }
        }

        private async void OnConnect(WebSocketConnection connection)
        {
            Console.WriteLine($"Connected with {connection}");

            connection.OnMessage = OnMessage;
            connection.OnError = OnError;
            connection.OnClose = OnClose;

            this.connection = connection;

            // Create player for this connection
            Guid newPlayerId = logic.AddPlayer();

            JoinResponse joinResponse = new JoinResponse
            {
                Header = Headers.JoinResponse,
                GuidForPlayer = newPlayerId,
            };
            await connection.SendAsync(Serializer.Serialize(joinResponse));
        }

        private async void OnMessage(string message)
        {
            if (connection == null) return;

            string header = Serializer.GetHeader(message);
            if (header == null) return;

            if (header == Headers.MovePlayerCommand)
            {
                MovePlayerCommand cmd = Serializer.Deserialize<MovePlayerCommand>(message);

                MovePlayerResponse response = new MovePlayerResponse
                {
                    TransactionId = cmd.TransactionId
                };
                try
                {
                    logic.MovePlayer(cmd.PlayerId, (ServerLogic.MoveDirection)cmd.Direction);
                    response.IsSuccess = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex} --- Failed to move the player");
                    response.IsSuccess = false;
                }
                await connection.SendAsync(Serializer.Serialize(response));

                UpdatePlayers();
                return;
            }
            if (header == Headers.GetPlayersCommand)
            {
                UpdatePlayers();
                return;
            }
        }

        private async void UpdatePlayers()
        {
            if (connection == null) { return; }
            UpdatePlayersResponse response = new UpdatePlayersResponse()
            {
                Header = Headers.UpdatePlayersResponse,
                Players = logic
                    .GetPlayers()
                    .Select(p => new PlayerData { Name = p.Name, X = p.X, Y = p.Y, Speed = p.Speed })
                    .ToArray()
            };
            await connection.SendAsync(Serializer.Serialize(response));
        }

        private void OnError()
        {
            Console.WriteLine("Connection errored out");
        }

        private void OnClose()
        {
            Console.WriteLine("Connection closed");
            connection = null;
        }

        private static async Task Main(string[] args)
        {
            Program program = new Program();
            await program.StartConnection();
        }
    }
}
