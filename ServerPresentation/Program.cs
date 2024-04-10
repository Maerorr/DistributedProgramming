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

            JoinResponse joinResponse = new JoinResponse(newPlayerId);
            await connection.SendAsync(Serializer.Serialize(joinResponse));
        }

        private async void OnMessage(string message)
        {
            if (connection == null) return;

            string header = Serializer.GetHeader(message);
            if (header == null) return;

            if (header == MovePlayerCommand.HEADER)
            {
                MovePlayerCommand cmd = Serializer.Deserialize<MovePlayerCommand>(message);

                MovePlayerResponse response = new MovePlayerResponse();
                response.transactionId = cmd.transactionId;
                try
                {
                    logic.MovePlayer(cmd.playerId, (ServerLogic.MoveDirection)cmd.direction);
                    response.isSuccess = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex} --- Failed to move the player");
                    response.isSuccess = false;
                }
                await connection.SendAsync(Serializer.Serialize(response));

                UpdatePlayers();
            }
            if (header == GetPlayersCommand.HEADER)
            {
                UpdatePlayers();
            }
        }

        private async void UpdatePlayers()
        {
            UpdatePlayersResponse response = new UpdatePlayersResponse();
            response.players = logic
                .GetPlayers()
                .Select(p => new PlayerData { name = p.Name, x = p.X, y = p.Y, speed = p.Speed })
                .ToArray();
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
