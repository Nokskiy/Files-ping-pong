using System.Numerics;

namespace FilesGame.Objects;

public class Ball : Object
{
    private Vector2 _velocity = new (0,1);

    public Ball()
    {
        RenderSymbol = "B";
    }
    
    public override void Update()
    {
        Position += _velocity;
    }
}