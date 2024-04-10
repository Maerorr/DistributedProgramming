using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Numerics;

namespace ClientData
{
    public interface IConnectionService
    {
        public event Action<string>? Log;
        public event Action? OnConnectionStateChanged;

        public event Action<string>? OnMessage;
        public event Action? OnError;
        public event Action? OnDisconnect;


        public Task Connect(Uri peerUri);
        public Task Disconnect();

        public bool IsConnected();

        public Task SendAsync(string message);
    }

    public interface IPlayer
    {
        public string Name { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Speed { get; set; }

        public static IPlayer Create(string name, float x, float y, float speed)
        {
            return new Player(name, x, y, speed);
        }
    }

    public enum MoveDirection
    {
        Up,
        Down,
        Left,
        Right,
    }

    public interface IData : IObservable<List<IPlayer>>
    {
        public IConnectionService ConnectionService { get; }
        public abstract List<IPlayer> GetPlayers();

        public static IData Create(IConnectionService? connection)
        {
            return new Data(connection);
        }

        public Task MovePlayer(MoveDirection direction);
        public void RequestUpdate();
    }
}
