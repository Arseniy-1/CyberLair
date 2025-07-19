using UnityEngine;

namespace Project.Scripts.Interfaces
{
    public interface IStunable
    {
        public Rigidbody2D Rigidbody2D { get; }
    
        public void TakeStun(float stunTime);
    }
}