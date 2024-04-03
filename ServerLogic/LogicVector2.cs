
using ServerData;

namespace ServerLogic
{
    internal class LogicVector2 : ILogicVector2
    {
        public float X { get; set; }
        public float Y { get; set; }

        public LogicVector2(IVector2 vector)
        {
            this.X = vector.X;
            this.Y = vector.Y;
        }
    }
}
