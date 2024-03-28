using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Reflection;

namespace Data;

internal class DataStorage : DataStorageAbstract
{
    private ObservableCollection<IPlayer> players = new ObservableCollection<IPlayer>();
    
    public override void Add(IPlayer player)
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

    public override List<IPlayer> GetPlayers()
    {
        return new List<IPlayer>(players);
    }
}