using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.EnemySystem.AttackTypes
{
    public class EnemyExplosionAttacker : EnemyAttacker
    {
        [SerializeField] private float _explosionRange;
        [SerializeField] private LayerMask _layerMask;
        
        protected override void Attack()
        {
            foreach (IDamageable health in GetCollidedBodies(Position))
            {
                health.TakeDamage(Damage);
            }
        }

        protected override void EndAttack()
        {
            Destroy(gameObject);
        }

        private List<IDamageable> GetCollidedBodies(Vector3 position)
        {
            Collider[] hits = Physics.OverlapSphere(position, _explosionRange, _layerMask);

            List<IDamageable> affected = new List<IDamageable>();

            foreach (Collider hit in hits)
            {
                if (hit.TryGetComponent(out IDamageable damagable))
                {
                    affected.Add(damagable);
                }
            }

            return affected;
        }
    }
}