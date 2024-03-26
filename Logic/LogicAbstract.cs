using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Data;

namespace Logic
{
    public abstract class LogicAbstract : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public abstract bool AddPlayer(String name);
        public abstract bool RemovePlayer(String name);
        public abstract int GetPlayerCount();
        public abstract void MovePlayer(string dir);

        public abstract ObservableCollection<Data.Player> GetObservableCollection();

        public static LogicAbstract CreateInstance(Action updateCallback, DataStorageAbstract? dataStorage = null)
        {
            return new Logic(dataStorage ?? DataStorageAbstract.CreateInstance(), updateCallback);
        }

        /*protected void OnPropertyChanged(ObservableCollection<Player> players, [CallerMemberName] string name = null)
        {
            PropertyChanged.Invoke(players, new PropertyChangedEventArgs());
        }*/
    }
}
