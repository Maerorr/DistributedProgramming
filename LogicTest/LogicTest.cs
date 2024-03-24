using Data;
using Logic;

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
            players.RemoveAll(p => p.Name == name);
        }
        
        public override int GetPlayerCount()
        {
            return players.Count;
        }
    }
    
    [TestClass]
    public class LogicTest
    {
        [TestMethod]
        public void AddPlayerTest()
        {
            var dataStorage = new DataStorageForTest();
            var logic = LogicAbstract.CreateInstance(dataStorage);
            logic.AddPlayer("Player");
            Assert.AreEqual(1, logic.GetPlayerCount());
        }
    }
}