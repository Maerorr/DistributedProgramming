using ClientData;

namespace ClientLogic
{
    public interface ILogicConnectionService
    {
        public event Action<string>? Log;
        public event Action? OnConnectionStateChanged;

        public event Action<string>? OnMessage;
        public event Action? OnError;
        public event Action? OnDisconnect;


        public Task Connect(Uri peerUri);
        public Task Disconnect();
        public bool IsConnected();
    }

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

        public static ILogic Create(IData? data = null)
        {
            IData dataApi = data ?? IData.Create(null);
            return new Logic(dataApi);
        }

        public List<ILogicPlayer> GetPlayers();
        public Task MovePlayer(MoveDirection moveDirection);
    }
}
