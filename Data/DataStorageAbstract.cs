using System.Collections.ObjectModel;

namespace Data;

public abstract class DataStorageAbstract
{
    public abstract void Add(Player player);
    public abstract void Remove(string name);
    public abstract int GetPlayerCount();
    public abstract ObservableCollection<Player> GetAll();

    public static DataStorageAbstract? CreateInstance()
    {
        return new DataStorage();
    }
}