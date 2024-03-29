﻿using Data;
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
        UpdateReactiveElements();
    }

    public override bool AddPlayer(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return false;
        }
        
        var player = IPlayer.Create(name, IVector2.Create(100, 100), 20.0f);
        player.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(UpdatePlayer);
        _dataStorage.Add(player);

        UpdatePlayers(this, null);
        
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
        var input = IInput.Create(dir);
        foreach (var player in players)
        {
            player.Move(input);
        }
    }

    private void UpdatePlayer(object sender, PropertyChangedEventArgs e)
    {
        _updateCallback.Invoke();
    }

    private void UpdatePlayers(object sender, NotifyCollectionChangedEventArgs e)
    {
        _updateCallback.Invoke();
    }

    public override ObservableCollection<IPlayer> GetObservableCollection()
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