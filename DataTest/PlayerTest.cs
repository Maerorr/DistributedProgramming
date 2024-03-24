using Data;

namespace DataTest
{
    [TestClass]
    public class PlayerTest
    {
        [TestMethod]
        public void UpdateTest()
        {
            var player = new Player("Player", new Vector2(0, 0), 1);
            player.HandleInput(new Player.Input { Up = true });
            player.Update();
            Assert.AreEqual(0, player.Position.X);
        }
    }
}