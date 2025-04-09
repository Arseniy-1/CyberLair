using System;
using UnityEngine;

public abstract class CollisionHandler : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleCollision(collision);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleCollision(collision.collider);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        HandleCollision(other.collider);
    }

    protected abstract void HandleCollision(Collider2D collider);
}