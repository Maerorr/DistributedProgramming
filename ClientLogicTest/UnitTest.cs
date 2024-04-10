using ClientData;
using ClientLogic;

namespace ClientLogicTest
{
    internal class DataMock : IData
    {
        public IConnectionService ConnectionService { get; set; }

        public event Action PlayersChanged;

        public List<IPlayer> GetPlayers()
        {
            return new List<IPlayer>();
        }

        public Task MovePlayer(ClientData.MoveDirection direction)
        {
            PlayersChanged.Invoke();
            return Task.CompletedTask;
        }
    }


    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void CreateTest()
        {
            ILogic logic = ILogic.Create(null, new DataMock());
            Assert.IsNotNull(logic);
        }

        [TestMethod]
        public void UpdateTest()
        {
            bool changed = false;
            Action action = () => { changed = true; };

            ILogic logic = ILogic.Create(action, new DataMock());
            logic.MovePlayer(ClientLogic.MoveDirection.Up);
            Assert.IsTrue(changed);
        }
    }
}