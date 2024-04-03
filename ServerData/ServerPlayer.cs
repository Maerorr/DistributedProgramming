using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ServerData;

internal class ServerPlayer : IServerPlayer
{
    public String Name { get; private set; }
    public IVector2 Position { get; private set; }
    
    public event PropertyChangedEventHandler? PropertyChanged;
    
    public ServerPlayer(String name, IVector2 position)
    {
        this.Name = name;
        this.Position = position;
    }

    public float Diameter
    {
        get { return 50.0f; }
    }

    public float X
    {
        get { return Position.X - Diameter / 2.0f; }
    }

    public float Y
    {
        get { return Position.Y - Diameter / 2.0f; }
    }
    
    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    public void Move(float x, float y)
    {
        Position.X += x;
        Position.Y += y;
        OnPropertyChanged(nameof(X));
        OnPropertyChanged(nameof(Y));
    }
}