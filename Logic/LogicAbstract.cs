using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Data;

namespace Logic
{
    public abstract class LogicAbstract
    {
        public abstract bool AddPlayer(String name);
        public abstract bool RemovePlayer(String name);
        public abstract int GetPlayerCount();
        public abstract void MovePlayer(string dir);

        public abstract ObservableCollection<Data.Player> GetObservableCollection();

        public static LogicAbstract CreateInstance(
            Action playerUpdateCallback,
            Action<bool> reactiveElementsUpdateCallback,
            DataStorageAbstract? dataStorage = null)
        {
            return new Logic(
                dataStorage ?? DataStorageAbstract.CreateInstance(),
                playerUpdateCallback,
                reactiveElementsUpdateCallback
                );
        }
    }
}
