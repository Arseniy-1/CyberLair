using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project.Scripts.EnemySystem.Bosses
{
    public abstract class ColliderAttack : BossAttack
    {
        [SerializeField] private Vector2 _offset;
        [SerializeField] private Vector2 _size;
        [SerializeField] private LayerMask _layerMask;

        protected override void Disable()
        {
            View.gameObject.SetActive(false);
        }

        protected override IEnumerator Attack()
        {
            List<Collider2D> affectedColliders = Physics2D
                .OverlapBoxAll((Vector2)transform.position + _offset, _size, _layerMask).ToList();

            foreach (Collider2D collider in affectedColliders)
            {
                if (collider.TryGetComponent(out IDamageable damageable))
                    damageable.TakeDamage(Damage);
            }
            
            Disable();
            
            yield return null;
        }

        protected void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            
            Gizmos.DrawWireCube((Vector2)transform.position + _offset, _size);
        }
    }
}