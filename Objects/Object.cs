using System.Numerics;

namespace FilesGame.Objects;

public class Object
{
    public Vector2 Position { get; set; }
    public string? RenderSymbol {get; set;}

    public virtual void Update()
    {
        
    }
}