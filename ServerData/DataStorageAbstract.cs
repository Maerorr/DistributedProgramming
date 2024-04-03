using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace ServerData;

public abstract class DataStorageAbstract
{
    public abstract void Add(IServerPlayer player);
    public abstract void Remove(string name);
    public abstract int GetPlayerCount();
    public abstract ObservableCollection<IServerPlayer> GetAll();

    public abstract void AddSubscriber(Action<object, NotifyCollectionChangedEventArgs> subscriber);
    public static DataStorageAbstract? CreateInstance()
    {
        return new DataStorage();
    }
}