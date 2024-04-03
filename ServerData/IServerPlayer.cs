using System.ComponentModel;

namespace ServerData;

public interface IServerPlayer : INotifyPropertyChanged
{
    float Diameter { get; }
    float X { get; }
    float Y { get; }
    
    string Name { get; }
    
    IVector2 Position { get; }
    
    public static IServerPlayer Create(String name, IVector2 position)
    {
        return new ServerPlayer(name, position);
    }

    public void Move(float x, float y);
}