using Data;

namespace DataTest
{
    [TestClass]
    public class PlayerTest
    {
        [TestMethod]
        public void MoveTest()
        {
            var player = new Player("Player", new Vector2(0, 0), 1);
            Assert.AreEqual(player.Position, new Vector2(0, 0));
            player.Move(Player.Input.Up);
           
            Assert.AreEqual(0, player.Position.X);
            // y is down, so -speed
            Assert.AreEqual(-player.Speed, player.Position.Y);
        }
    }
}