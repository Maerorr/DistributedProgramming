using ServerData;

namespace ServerDataTest
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void AddPlayerTest()
        {
            IData data = IData.Create();

            bool changed = false;
            data.PlayersChanged += () => { changed = true; };

            List<IPlayer> players = data.GetPlayers();
            Assert.IsNotNull(players);
            Assert.AreEqual(players.Count, 0);

            string name = "Andrew";
            float x = 50.0f;
            float y = 60.0f;
            float speed = 13.0f;

            Guid playerId = data.AddPlayer(name, x, y, speed);

            Assert.IsTrue(changed);

            players = data.GetPlayers();
            Assert.IsNotNull(players);
            Assert.AreEqual(players.Count, 1);

            Assert.AreEqual(players[0].Name, name);
            Assert.AreEqual(x, players[0].X, 0.01);
            Assert.AreEqual(y, players[0].Y, 0.01);
            Assert.AreEqual(speed, players[0].Speed, 0.01);

            data.MovePlayer(playerId, MoveDirection.Up);
            
            players = data.GetPlayers();
            Assert.IsNotNull(players);
            Assert.AreEqual(players.Count, 1);

            Assert.AreEqual(x, players[0].X, 0.01);
            Assert.AreEqual(y - speed, players[0].Y, 0.01);
        }
    }
}