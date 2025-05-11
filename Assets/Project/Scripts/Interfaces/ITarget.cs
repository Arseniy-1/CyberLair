using System;
using UnityEngine;

public interface ITarget
{
    public event Action OnDeath;
    
    public Vector2 Position { get; }
}