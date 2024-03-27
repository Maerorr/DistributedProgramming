using Data;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;

namespace Logic;

internal class Logic : LogicAbstract
{
    private DataStorageAbstract _dataStorage;

    private Action _updateCallback;
    private Action<bool> _reactiveElementsUpdateCallback;

    public Logic(DataStorageAbstract? dataStorage, Action playerUpdateCallback, Action<bool> reactiveElementsUpdateCallback)
    {
        this._dataStorage = dataStorage;
        this._updateCallback = playerUpdateCallback;
        this._reactiveElementsUpdateCallback = reactiveElementsUpdateCallback;
        AddPlayer("test");
        UpdateReactiveElements();
    }

    public override bool AddPlayer(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return false;
        }

        var player = new Player(name, new Vector2(100, 100), 20.0f);
        player.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(UpdatePlayer);
        _dataStorage.Add(player);
        
        return true;
    }
    
    public override bool RemovePlayer(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return false;
        }
        
        _dataStorage.Remove(name);
        return true;
    }
    
    public override int GetPlayerCount()
    {
        return _dataStorage.GetPlayerCount();
    }

    public override void MovePlayer(string dir)
    {
        var players = _dataStorage.GetAll();
        Data.Player.Input input = Player.Input.Up;
        if (dir == "down")
        {
            input = Player.Input.Down;
        } else if (dir == "left")
        {
            input = Player.Input.Left;
        } else if (dir == "right")
        {
            input = Player.Input.Right;
        }
        foreach (var player in players)
        {
            player.Move(input);
        }

        //_updateCallback.Invoke();
    }

    private void UpdatePlayer(object sender, PropertyChangedEventArgs e)
    {
        _updateCallback.Invoke();
    }

    private void UpdatePlayers(object sender, NotifyCollectionChangedEventArgs e)
    {
        _updateCallback.Invoke();
    }

    public override ObservableCollection<Player> GetObservableCollection()
    {
        return _dataStorage.GetAll();
    }

    private async void UpdateReactiveElements()
    {
        // simple reactive change of color
        bool greenOrBlue = false;
        while (true)
        {
            await Task.Delay(1000);
            greenOrBlue = !greenOrBlue;
            _reactiveElementsUpdateCallback.Invoke(greenOrBlue);
        }
    }
}