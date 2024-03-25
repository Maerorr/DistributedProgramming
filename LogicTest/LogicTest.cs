using Data;
using Logic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace LogicTest
{
    public class DataStorageForTest : DataStorageAbstract
    {
        public List<Player> players = new List<Player>();

        public override void Add(Player player)
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

        public override ObservableCollection<Player> GetAll()
        {
            return new ObservableCollection<Player>(players);
        }
    }

    [TestClass]
    public class LogicTest
    {
        // created just to pass as needed "Action" parameter that is called every time a player is moved
        public void DoNothing() { }

        [TestMethod]
        public void AddPlayerTest()
        {
            var dataStorage = new DataStorageForTest();
            // currently, Logic creates a new player in the constructor
            var logic = LogicAbstract.CreateInstance(DoNothing, dataStorage);
            logic.AddPlayer("Player");
            // we need to compensate for the initial player created in the constructor
            Assert.AreEqual(2, logic.GetPlayerCount());
        }

        [TestMethod]
        public void RemovePlayerTest()
        {
            var dataStorage = new DataStorageForTest();
            var logic = LogicAbstract.CreateInstance(DoNothing, dataStorage);
            // the initial player added in the constructor
            logic.RemovePlayer("test");
            Assert.AreEqual(0, logic.GetPlayerCount());

            logic.AddPlayer("Player");
            logic.RemovePlayer("Player");
            Assert.AreEqual(0, logic.GetPlayerCount());
        }

        [TestMethod]
        public void MovePlayerTest()
        {
            var dataStorage = new DataStorageForTest();
            var logic = LogicAbstract.CreateInstance(DoNothing, dataStorage);

            logic.AddPlayer("Player");
            var collection = logic.GetObservableCollection();
            var player = collection.Where(p => p.Name == "Player").Single();
            var playerSpeed = 20f;
            var initialPosition = new Vector2(100, 100);

            logic.MovePlayer("down");
            Assert.AreEqual(initialPosition.Y + playerSpeed, player.Position.Y);
            logic.MovePlayer("left");
            Assert.AreEqual(initialPosition.X - playerSpeed, player.Position.X);
            logic.MovePlayer("right");
            Assert.AreEqual(initialPosition.X, player.Position.X);
        }   
    }
}