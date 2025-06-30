using UnityEngine;

public struct M_Exploded
{    
    public M_Exploded(Vector2 position)
    {
        Position = position;
    }
    
    public Vector2 Position { get; private set; }
}