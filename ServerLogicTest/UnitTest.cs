using ServerData;
using ServerLogic;

namespace ServerLogicTest
{
    internal class MockData : IData
    {
        public static Guid testGuid = new Guid();

        public event Action PlayersChanged;

        public Guid AddPlayer(string name, float x, float y, float speed)
        {
            return testGuid;
        }

        public List<IPlayer> GetPlayers()
        {
            return new List<IPlayer>();
        }

        public bool HasPlayer(Guid playerId)
        {
            return false;
        }

        public void MovePlayer(Guid playerId, ServerData.MoveDirection direction)
        {
            PlayersChanged.Invoke();
        }
    }

    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void CreateTest()
        {
            ILogic logic = ILogic.Create(null, new MockData());
            Assert.IsNotNull(logic);
        }

        [TestMethod]
        public void MoveExceptionTest()
        {
            ILogic logic = ILogic.Create(null, new MockData());

            Action act = () => { logic.MovePlayer(new Guid(), ServerLogic.MoveDirection.Up); };

            Assert.ThrowsException<KeyNotFoundException>(act);
        }

        [TestMethod]
        public void AddPlayerTest()
        {
            ILogic logic = ILogic.Create(null, new MockData());

            Assert.AreEqual(MockData.testGuid, logic.AddPlayer());
        }
    }
}