using ClientLogic;
using Logic;

namespace ClientPresentation.Model
{
    public class ModelVector2
    {
        public float X { get; }
        public float Y { get; }

        public ModelVector2(ILogicVector2 vec2)
        {
            this.X = vec2.X;
            this.Y = vec2.Y;
        }
    }
}
