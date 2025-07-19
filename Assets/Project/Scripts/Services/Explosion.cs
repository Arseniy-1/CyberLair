using System;
using System.Collections.Generic;
using System.Linq;
using Project.Scripts.Interfaces;
using Project.Scripts.MessageBroker;
using UnityEngine;

namespace Project.Scripts.Services
{
    [Serializable]
    public class Explosion
    {
        private const int MaxHits = 4;
        
        private readonly Collider2D[] _results = new Collider2D[MaxHits];
        
        [SerializeField] private float _range;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private int _explosionDamage;
        
        public void Explode(Vector3 position)
        {
            int hitCount = Physics2D.OverlapCircleNonAlloc(position, _range, _results, _layerMask);
            
            List<IDamageable> affected = _results
                .Take(hitCount)
                .Select(hit =>
                {
                    hit.TryGetComponent(out IDamageable health);
                    
                    return health;
                })
                .Where(health => health != null)
                .ToList();

            foreach (IDamageable hit in affected)
            {
                hit.TakeDamage(_explosionDamage);
            }

            MessageBrokerHolder.Game.Publish(new M_Exploded(position));
        }
    }
}