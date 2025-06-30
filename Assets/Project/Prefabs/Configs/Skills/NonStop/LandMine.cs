using System;
using UnityEngine;

public class LandMine : MonoBehaviour, IDestoyable<LandMine>
{
    [SerializeField] private float _damage;
    [SerializeField] private float _stunTime;

    public event Action<LandMine> OnDestroyed;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IDamageable damageable) == false) 
            return;
        
        if (other.TryGetComponent(out IStunable stunable))
            stunable.TakeStun(_stunTime);
            
        damageable.TakeDamage(_damage);
        
        MessageBrokerHolder.Game
            .Publish(new M_Exploded(transform.position));
        
        OnDestroyed?.Invoke(this);
    }
}