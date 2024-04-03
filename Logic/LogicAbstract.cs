using ClientLogic;
using Data;

namespace Logic
{
    public abstract class LogicAbstract
    {
        public abstract bool AddPlayer(String name);
        public abstract bool RemovePlayer(String name);
        public abstract int GetPlayerCount();
        public abstract void MovePlayer(string dir);

        public abstract List<ILogicPlayer> GetPlayers();

        public event Action playersUpdated;

        public abstract void RequestUpdate();

        public static LogicAbstract CreateInstance(
            Action playerUpdateCallback,
            Action<bool> reactiveElementsUpdateCallback,
            DataStorageAbstract? dataStorage = null)
        {
            return new Logic(
                dataStorage ?? DataStorageAbstract.CreateInstance(null),
                playerUpdateCallback,
                reactiveElementsUpdateCallback
                );
        }

        public abstract void OnPlayersUpdated();

        public abstract ILogicConnectionHandler GetConnectionHandler();
    }

    public interface ILogicVector2
    {
        float X { get; }
        float Y { get; }
    }

    public interface ILogicPlayer
    {
        float Diameter { get; }
        float X { get; }
        float Y { get; }

        string Name { get; }

        ILogicVector2 Position { get; }
    }

    public interface ILogicConnectionHandler
    {
        event Action<string> log;
        event Action<string> onMessage;
        event Action onClose;
        event Action onError;

        Task Connect(Uri peer);
        Task Disconnect();
        bool IsConnected();
    }
}
