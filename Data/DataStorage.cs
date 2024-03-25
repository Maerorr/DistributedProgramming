using System.Collections.ObjectModel;

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

    public override ObservableCollection<Player> GetAll()
    {
        return new ObservableCollection<Player>(players);
    }
}