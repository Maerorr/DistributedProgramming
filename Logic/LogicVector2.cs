using Data;
using Logic;

namespace ClientLogic
{
    internal class LogicVector2 : ILogicVector2
    {
        public float X { get; }
        public float Y { get; }

        public LogicVector2(IVector2 vector2)
        {
            this.X = vector2.X;
            this.Y = vector2.Y;
        }
    }
}
