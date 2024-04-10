using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientData
{
    internal class Player : IPlayer
    {
        public string Name { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Speed { get; set; }

        public Player(string name, float x, float y, float speed)
        {
            Name = name;
            X = x;
            Y = y;
            Speed = speed;
        }
    }
}
