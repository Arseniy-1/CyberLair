using System;
using UnityEngine;

public class LandMine : MonoBehaviour, IDestoyable<LandMine>
{
    [SerializeField] private float _damage;
    [SerializeField] private float _stunTime;

    public event Action<LandMine> OnDestroyed;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            if (other.TryGetComponent(out IStunable stunable))
                stunable.TakeStun(_stunTime);
            
            damageable.TakeDamage(_damage);
            
            OnDestroyed?.Invoke(this);
        }
    }
}