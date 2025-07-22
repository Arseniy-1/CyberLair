using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Project.Scripts.Interfaces;
using Project.Scripts.Services.Enum;
using Project.Scripts.Services.Extensions;
using UnityEngine;

namespace Project.Scripts.EnemySystem.Bosses.Attacks
{
    public abstract class ColliderAttack : BossAttack
    {
        private const int MaxHits = 4;
        
        private readonly Collider2D[] _results = new Collider2D[MaxHits];
        
        [SerializeField] private Vector2 _offset;
        [SerializeField] private Vector2 _size;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private ShakeID _shakeID;
        [SerializeField] private Transform _bossViewScale;

        public override void Disable()
        {
            View.gameObject.SetActive(false);
        }

        protected override IEnumerator Attack()
        {
            Vector2 overlapCenter = (Vector2)transform.position + _offset * _bossViewScale.localScale.x;
            
            int hitCount = Physics2D.OverlapBoxNonAlloc(overlapCenter, _size, 0f, _results, _layerMask);
            
            List<IDamageable> affected = _results
                .Take(hitCount)
                .Select(hit =>
                {
                    hit.TryGetComponent(out IDamageable health);
                    
                    return health;
                })
                .Where(health => health != null)
                .ToList();
            
            _shakeID.Shake();

            foreach (IDamageable hit in affected)
            {
                hit.TakeDamage(Damage);
            }
            
            Disable();
            
            yield return null;
        }
    }
}