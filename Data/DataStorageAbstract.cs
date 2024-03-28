using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Data;

public abstract class DataStorageAbstract
{
    public abstract void Add(IPlayer player);
    public abstract void Remove(string name);
    public abstract int GetPlayerCount();
    public abstract List<IPlayer> GetPlayers();

    public abstract void AddSubscriber(Action<object, NotifyCollectionChangedEventArgs> subscriber);
    public static DataStorageAbstract? CreateInstance()
    {
        return new DataStorage();
    }
}