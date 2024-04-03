using GlobalAPIs;
using Newtonsoft.Json;
using ServerData;
using ServerLogic;

namespace ServerPresentation;

internal class Program
{

    private WebSocketConnection? ws;
    private readonly LogicAbstract logic;

    private Program(LogicAbstract logicAbstract)
    {
        this.logic = logicAbstract;
    }

    private async Task Connect()
    {
        while(true)
        {
            Console.WriteLine("Connecting . . .");
            await WebSocketServer.StartServer(9998, OnConnect);
        }
    }

    private void OnConnect(WebSocketConnection connection)
    {
        Console.WriteLine($"Connected to: {connection}");
        connection.onMessage = OnMessage;
        connection.onError = OnError;
        connection.onClose = OnClose;
    }

    private async void OnMessage(string message)
    {
        if (ws == null)
            return;

        Console.WriteLine($"Received message: {message}");

        if (Serializer.GetCommandHeader(message) == GetPlayersCommand.HEADER)
        {
            GetPlayersCommand getPlayersCommand = Serializer.Deserialize<GetPlayersCommand>(message);
            await SendPlayers();
        }
        else if (Serializer.GetCommandHeader(message) == MovePlayerCommand.HEADER)
        {
            MovePlayerCommand movePlayerCommand = Serializer.Deserialize<MovePlayerCommand>(message);
            logic.MovePlayer(movePlayerCommand.name, movePlayerCommand.x, movePlayerCommand.y);
        }
    }

    private async void OnClose()
    {
        ws = null;
        Console.WriteLine($"Connection closed: {ws}");
    }

    private async Task SendPlayers()
    {
        if (ws == null)
            return;

        UpdatePlayersResponse response = new UpdatePlayersResponse();
        List<ILogicPlayer> playes = logic.GetPlayers();
        response.players = playes.Select(p => new PlayerData(p.Name, p.Position.X, p.Position.Y)).ToArray();

        string responseJson = JsonConvert.SerializeObject(response);
        await ws.SendAsync(responseJson);
    }

    private async void OnError()
    {
        if (ws == null)
            return;

        Console.WriteLine($"Error on connection: {ws}");
    }

    private static async Task Main(string[] args)
    {
        Program program = new Program(LogicAbstract.CreateInstance());
        await program.Connect();
    }
}