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
            foreach (IDamagable health in GetCollidedBodies(Position))
            {
                health.TakeDamage(Damage);
            }
        }

        protected override void EndAttack()
        {
            Destroy(gameObject);
        }

        private List<IDamagable> GetCollidedBodies(Vector3 position)
        {
            Collider[] hits = Physics.OverlapSphere(position, _explosionRange, _layerMask);

            List<IDamagable> affected = new List<IDamagable>();

            foreach (Collider hit in hits)
            {
                if (hit.TryGetComponent(out IDamagable damagable))
                {
                    affected.Add(damagable);
                }
            }

            return affected;
        }
    }
}