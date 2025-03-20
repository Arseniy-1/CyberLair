using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.EnemySystem.Bosses.DeathReaper
{
    public class SlashAttack : BossAttack
    {
        [SerializeField] private Collider2D _hitbox;
        [SerializeField] private ContactFilter2D _filter;
        
        public override void Initialize()
        {
            BossAttackAnimationTrigger = Animator.StringToHash("SlashAttack");
            
            Disable();
        }

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
            
            yield return null;
        }
    }
}