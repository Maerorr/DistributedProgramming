using ClientData;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using GlobalAPIs;

namespace Data;

internal class DataStorage : DataStorageAbstract
{
    private ObservableCollection<IPlayer> players = new ObservableCollection<IPlayer>();
    private readonly object playersLock = new object();

    private IConnectionHandler connectionHandler;

    public event Action playersUpdated;

    public DataStorage(IConnectionHandler? connectionHandler)
    {
        this.connectionHandler = connectionHandler ?? new ConnectionHandler();
    }

    public async Task RequestPlayersUpdate()
    {
        await connectionHandler.SendAsync(JsonConvert.SerializeObject(new GetPlayersCommand()));
    }

    public async override void RequestUpdate()
    {
        if (connectionHandler.IsConnected())
            await RequestPlayersUpdate();
    }

    private void UpdatePlayers(UpdatePlayersResponse response)
    {
        if (response.players == null)
        {
            return;
        }

        lock(playersLock)
        {
            players.Clear();
            foreach (var player in response.players)
            {
                players.Add(IPlayer.Create(player.name, IVector2.Create(player.x, player.y), 20.0f));
            }
        }
        playersUpdated?.Invoke();
    }

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

    public override ObservableCollection<IPlayer> GetAll()
    {
        return new ObservableCollection<IPlayer>(players);
    }

    public override IConnectionHandler GetConnectionHandler()
    {
        return connectionHandler;
    }
}