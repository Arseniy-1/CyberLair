using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.EnemySystem.Bosses
{
    public abstract class ColliderAttack : BossAttack
    {
        [SerializeField] private Collider2D _hitbox;
        [SerializeField] private ContactFilter2D _filter;

        protected override void Disable()
        {
            View.gameObject.SetActive(false);
            _hitbox.enabled = false;
        }

        protected override IEnumerator Attack()
        {
            List<Collider2D> affectedColliders = new();
            
            _hitbox.enabled = true;
            Physics2D.OverlapCollider(_hitbox, _filter, affectedColliders);
            _hitbox.enabled = false;

            foreach (Collider2D collider in affectedColliders)
            {
                if (collider.TryGetComponent(out IDamageable damageable))
                    damageable.TakeDamage(Damage);
            }
            
            Disable();
            
            yield return null;
        }
    }
}