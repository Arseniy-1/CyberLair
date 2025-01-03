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
            foreach (Health health in GetCollidedBodies(Position))
            {
                health.TakeDamage(Damage);
            }
        }

        protected override void EndAttack()
        {
            Destroy(gameObject);
        }

        private List<Health> GetCollidedBodies(Vector3 position)
        {
            Collider[] hits = Physics.OverlapSphere(position, _explosionRange, _layerMask);

            List<Health> affected = new List<Health>();

            foreach (Collider hit in hits)
            {
                if (hit.TryGetComponent(out Health health))
                {
                    affected.Add(health);
                }
            }

            return affected;
        }
    }
}