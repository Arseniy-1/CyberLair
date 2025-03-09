using System.Collections;
using Project.Scripts.Servises;
using UnityEngine;

namespace Project.Scripts.EnemySystem.Bosses.PerimeterSentinel
{
    public class ShieldTimedAttack : BossTimedAttack
    {
        [SerializeField] private SkillCollisionHandler _skillCollisionHandler;
        [SerializeField] private InvincibilityCollisionHandler _invincibilityCollisionHandler;
        [SerializeField] private Collider2D _shieldCollider;
        [SerializeField] private Enemy _boss;
        [SerializeField] private float _bossStunTime;

        private void OnEnable()
        {
            Disable();
        }

        protected override void Disable()
        {
            _shieldCollider.enabled = false;
            _skillCollisionHandler.enabled = false;
            
            _invincibilityCollisionHandler.enabled = false;
            View.gameObject.SetActive(false);
        }

        protected override IEnumerator Attack()
        {
            _shieldCollider.enabled = true;
            _skillCollisionHandler.enabled = true;
            _invincibilityCollisionHandler.enabled = true;
            View.gameObject.SetActive(true);

            _skillCollisionHandler.ContactLimitExpired += Stun;

            Animator.SetTrigger(AttackTrigger);

            yield return null;
        }

        private void Stun()
        {
            _skillCollisionHandler.ContactLimitExpired -= Stun;
            _boss.TakeStun(_bossStunTime);
            
            Disable();
        }
    }
}