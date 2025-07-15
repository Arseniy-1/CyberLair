using System;
using UnityEngine;

namespace Project.Scripts.Interfaces
{
    public interface ITarget
    {
        public event Action OnDeath;
    
        public Vector2 Position { get; }
    }
}