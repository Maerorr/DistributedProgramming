using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Data;

public abstract class DataStorageAbstract
{
    public abstract void Add(IPlayer player);
    public abstract void Remove(string name);
    public abstract int GetPlayerCount();
    public abstract ObservableCollection<IPlayer> GetAll();

    public event Action playersUpdated;

    public abstract void AddSubscriber(Action<object, NotifyCollectionChangedEventArgs> subscriber);
    public static DataStorageAbstract? CreateInstance(IConnectionHandler? connectionHandler)
    {
        return new DataStorage(connectionHandler);
    }

    public abstract void RequestUpdate();

    public abstract IConnectionHandler GetConnectionHandler();
}

public interface IConnectionHandler
{
    public event Action<string> log;
    public event Action<string> onMessage;
    public event Action onClose;
    public event Action onError;

    public Task Connect(Uri peer);
    public Task Disconnect();
    public bool IsConnected();
    public Task SendAsync(string message);
}