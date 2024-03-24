namespace Data;

public class Player
{
    public String Name { get; private set; }
    public Vector2 Position { get; private set; }
    public float Speed { get; private set; }
    private Input _currentInput;

    public enum Input
    {
        Up,
        Down,
        Left,
        Right,
    }
    
    public Player(String name, Vector2 position, float speed)
    {
        this.Name = name;
        this.Position = position;
        this.Speed = speed;
    }

    public void HandleInput(Input input)
    {
        _currentInput = input;
    }
    
    public void Move(Input input)
    {
        switch (input)
        {
            case Input.Up:
                Position.Y -= Speed;
                break;
            case Input.Down:
                Position.Y += Speed;
                break;
            case Input.Left:
                Position.X -= Speed;
                break;
            case Input.Right:
                Position.X += Speed;
                break;
        }
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
}