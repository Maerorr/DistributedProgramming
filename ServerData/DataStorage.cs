using GlobalAPIs;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Net.WebSockets;
using System.Reflection;

namespace ServerData;

internal class DataStorage : DataStorageAbstract
{
    private ObservableCollection<IServerPlayer> players = new ObservableCollection<IServerPlayer>();
    
    public override void Add(IServerPlayer player)
    {
        players.Add(player);
    }
    
    public override void Remove(String name)
    {
        players.Remove(players.Where(i => i.Name == name).Single());
    }
    
    public override int GetPlayerCount()
    {
        return players.Count;
    }

    public override void AddSubscriber(Action<object, NotifyCollectionChangedEventArgs> subscriber)
    {
        players.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(subscriber);
    }

    public override ObservableCollection<IServerPlayer> GetAll()
    {
        return new ObservableCollection<IServerPlayer>(players);
    }
}