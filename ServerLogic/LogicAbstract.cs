using System;
using System.Collections.Generic;
using GlobalAPIs;
using ServerData;

namespace ServerLogic
{
    public abstract class LogicAbstract
    {
        public DataStorageAbstract dataStorage { get; private set; }

        public LogicAbstract(DataStorageAbstract dataStorage)
        {
            this.dataStorage = dataStorage;
        }

        public abstract List<ILogicPlayer> GetPlayers();

        public abstract void MovePlayer(string name, float x, float y);

        public static LogicAbstract CreateInstance(DataStorageAbstract dataStorage = null)
        {
            DataStorageAbstract data = dataStorage ?? DataStorageAbstract.CreateInstance();
            return new Logic(dataStorage);
        }
    }

    public interface ILogicVector2
    {
        float X { get; set; }
        float Y { get; set; }
    }

    public interface ILogicPlayer
    {
        float Diameter { get; }
        float X { get; }
        float Y { get; }

        string Name { get; }

        ILogicVector2 Position { get; set; }
    }
}
