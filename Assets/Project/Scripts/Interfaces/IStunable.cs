using UnityEngine;

public interface IStunable
{
    public Rigidbody2D Rigidbody2D { get; }
    
    void TakeStun(float stunTime);
}