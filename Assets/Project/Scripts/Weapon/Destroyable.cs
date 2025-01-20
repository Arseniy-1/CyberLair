using System;
using UnityEngine;

public abstract class Destroyable<T> : MonoBehaviour
{
    public event Action<T> OnDestroyed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleCollision(collision);
    }

    private void HandleCollision(Collider2D collider2D)
    {
        if (collider2D.TryGetComponent(out IDamagable damagable))
        {

        }
    }
}