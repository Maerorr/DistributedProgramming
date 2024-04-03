using Data;
using Logic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;

namespace LogicTest
{
    public class MockDataStorage : DataStorageAbstract
    {
        public List<IPlayer> players = new List<IPlayer>();

        public override void Add(IPlayer player)
        {
            players.Add(player);
        }

        public override void Remove(string name)
        {
            players.Remove(players.Where(i => i.Name == name).Single());
        }

        public override int GetPlayerCount()
        {
            return players.Count;
        }

        public override ObservableCollection<IPlayer> GetAll()
        {
            return new ObservableCollection<IPlayer>(players);
        }

        public override void AddSubscriber(Action<object, NotifyCollectionChangedEventArgs> subscriber)
        {
            // do nothing
        }

        public override IConnectionHandler GetConnectionHandler()
        {
            throw new NotImplementedException();
        }

        public override void RequestUpdate()
        {
            throw new NotImplementedException();
        }
    }

    [TestClass]
    public class LogicTest
    {
        // created just to pass as needed "Action" parameter that is called every time a player is moved
        public void DoNothing() { }
        public void DoNothing(bool b) { }

        [TestMethod]
        public void AddPlayerTest()
        {
            var dataStorage = new MockDataStorage();
            var logic = LogicAbstract.CreateInstance(DoNothing, DoNothing, dataStorage);
            logic.AddPlayer("Player");
            Assert.AreEqual(1, logic.GetPlayerCount());
        }

        [TestMethod]
        public void RemovePlayerTest()
        {
            var dataStorage = new MockDataStorage();
            var logic = LogicAbstract.CreateInstance(DoNothing, DoNothing, dataStorage);
            // the initial player added in the constructor
            Assert.AreEqual(0, logic.GetPlayerCount());

            logic.AddPlayer("Player");
            logic.RemovePlayer("Player");
            Assert.AreEqual(0, logic.GetPlayerCount());
        }

        [TestMethod]
        public void MovePlayerTest()
        {
            var dataStorage = new MockDataStorage();
            var logic = LogicAbstract.CreateInstance(DoNothing, DoNothing, dataStorage);

            logic.AddPlayer("Player");
            var collection = logic.GetPlayers();
            var player = collection.Where(p => p.Name == "Player").Single();
            var playerSpeed = 20f;
            var initialPosition = IVector2.Create(100, 100);

            logic.MovePlayer("down");
            Assert.AreEqual(initialPosition.Y + playerSpeed, player.Position.Y);
            logic.MovePlayer("left");
            Assert.AreEqual(initialPosition.X - playerSpeed, player.Position.X);
            logic.MovePlayer("right");
            Assert.AreEqual(initialPosition.X, player.Position.X);
        }   
    }
}