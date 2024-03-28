using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Data;

public interface IPlayer : INotifyPropertyChanged
{
    IInput _currentInput { get; }
    void Move(IInput direction);
    float Diameter { get; }
    float X { get; }
    float Y { get; }
    
    string Name { get; }
    
    IVector2 Position { get; }
    
    public static IPlayer Create(String name, IVector2 position, float speed)
    {
        return new Player(name, position, speed);
    }
}