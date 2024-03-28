using System;
using Data;

namespace Logic
{
    internal class LogicPlayer : ILogicPlayer
    {
        public string Name { get; }

        public float X { get; private set; }
        public float Y { get; private set; }

        public LogicPlayer(IPlayer player)
        {
            Name = player.Name;
            X = player.X;
            Y = player.Y;
        }
    }
}