namespace Data;

internal enum InputEnum
{
    Up,
    Down,
    Left,
    Right,
}

internal class Input : IInput
{
    InputEnum _input { get; set; }

    public string Direction
    {
        get
        {
            return _input.ToString().ToLower();
        }
    }
    
    public Input(string direction)
    {
        switch (direction)
        {
            case "up":
                this._input = InputEnum.Up;
                break;
            case "down":
                this._input = InputEnum.Down;
                break;
            case "left":
                this._input = InputEnum.Left;
                break;
            case "right":
                this._input = InputEnum.Right;
                break;
            default:
                throw new ArgumentException("Invalid direction");
        }
    }
    
    private Input(InputEnum input)
    {
        _input = input;
    }
    
}
