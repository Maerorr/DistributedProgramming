﻿using Data;
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
            /*var player = new Player("Player", new Vector2(0, 0), 1);
            player.PropertyChanged += DoNothing;
            Assert.AreEqual(player.Position, new Vector2(0, 0));
            player.Move(Input.Up);
           
            Assert.AreEqual(0, player.Position.X);
            // y is down, so -speed
            Assert.AreEqual(-player.Speed, player.Position.Y);*/
            // rewrite using IPlayer and IInput
            
            var player = IPlayer.Create("Player", IVector2.Create(0, 0), 1);
            player.PropertyChanged += DoNothing;
            Assert.AreEqual(player.Position, IVector2.Create(0, 0));
            player.Move(IInput.Create("up"));
            
        }
    }
}