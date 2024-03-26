using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Reflection;

namespace Data;

internal class DataStorage : DataStorageAbstract
{
    private ObservableCollection<Player> players = new ObservableCollection<Player>();
    
    public override void Add(Player player)
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

    public override ObservableCollection<Player> GetAll()
    {
        return new ObservableCollection<Player>(players);
    }
}