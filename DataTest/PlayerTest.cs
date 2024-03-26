using Data;
using System.ComponentModel;

namespace DataTest
{
    [TestClass]
    public class PlayerTest
    {
        void DoNothing(object sender, PropertyChangedEventArgs e) { }

        [TestMethod]
        public void MoveTest()
        {
            var player = new Player("Player", new Vector2(0, 0), 1);
            player.PropertyChanged += DoNothing;
            Assert.AreEqual(player.Position, new Vector2(0, 0));
            player.Move(Player.Input.Up);
           
            Assert.AreEqual(0, player.Position.X);
            // y is down, so -speed
            Assert.AreEqual(-player.Speed, player.Position.Y);
        }
    }
}