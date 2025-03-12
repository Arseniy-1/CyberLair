using System.Collections;
using Project.Scripts.Servises;
using UnityEngine;

namespace Project.Scripts.EnemySystem.Bosses.PerimeterSentinel
{
    public class ShieldTimedAttack : BossTimedAttack
    {
        [SerializeField] private Shield shield;
        [SerializeField] private SkillCollisionHandler _skillCollisionHandler;
        [SerializeField] private InvincibilityCollisionHandler _invincibilityCollisionHandler;
        [SerializeField] private Collider2D _shieldCollider;
        [SerializeField] private float _duration;
        
        [SerializeField, Header("Boss")] private Enemy _boss;
        [SerializeField] private float _bossStunTime;
        
        private Coroutine _timerCoroutine;

        public override void Initialize()
        {
            BossAttackAnimationTrigger = Animator.StringToHash("ShieldTimedAttack");
            
            Disable();
        }

        protected override void Disable()
        {
            if (_timerCoroutine != null)
            {
                StopCoroutine(_timerCoroutine);
                _timerCoroutine = null;
            }
            
            _shieldCollider.enabled = false;
            _skillCollisionHandler.enabled = false;
            
            _invincibilityCollisionHandler.enabled = false;
            View.gameObject.SetActive(false);
        }

        protected override IEnumerator Attack()
        {
            _skillCollisionHandler.ContactLimitExpired += Stun;
            AnimatorEvents.Attacking += Activate;
            
            View.gameObject.SetActive(true);
            AttackAnimator.SetTrigger(AttackTrigger);

            yield return null;
        }

        private void Stun()
        {
            _skillCollisionHandler.ContactLimitExpired -= Stun;
            _boss.TakeStun(_bossStunTime);
            
            Disable();
        }

        private void Activate()
        {
            AnimatorEvents.Attacking -= Activate;
            
            _shieldCollider.enabled = true;
            _skillCollisionHandler.enabled = true;
            _invincibilityCollisionHandler.enabled = true;

            _timerCoroutine = StartCoroutine(DisableTimer());
        }

        private IEnumerator DisableTimer()
        {
            yield return new WaitForSeconds(_duration);
            
            Disable();
        }
    }
}