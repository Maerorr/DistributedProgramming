using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Data;

internal class Player : IPlayer
{
    public String Name { get; private set; }
    public IVector2 Position { get; private set; }
    public float Speed { get; private set; }
    
    public IInput _currentInput { get; private set; }
    
    public event PropertyChangedEventHandler? PropertyChanged;
    
    public Player(String name, IVector2 position, float speed)
    {
        this.Name = name;
        this.Position = position;
        this.Speed = speed;
    }

    public void HandleInput(Input input)
    {
        _currentInput = input;
    }
    
    public void Move(IInput input)
    {
        switch (input.Direction)
        {
            case "up":
                Position.Y -= Speed;
                break;
            case "down":
                Position.Y += Speed;
                break;
            case "left":
                Position.X -= Speed;
                break;
            case "right":
                Position.X += Speed;
                break;
        }

        PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Position"));
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
}