using System.Collections;
using Project.Scripts.Services;
using UnityEngine;

namespace Project.Scripts.EnemySystem.Bosses.Attacks.PerimeterSentinel
{
    public class ShieldAttack : BossAttack
    {
        [SerializeField] private SkillCollisionHandler _skillCollisionHandler;
        [SerializeField] private InvincibilityCollisionHandler _invincibilityCollisionHandler;
        [SerializeField] private Collider2D _shieldCollider;
        [SerializeField] private float _duration;
        
        [SerializeField] [Header("Boss")] private Enemy _boss;
        [SerializeField] private EnemyCollisionHandler _enemyCollisionHandler;
        [SerializeField] private float _bossStunTime;
        
        private Coroutine _timerCoroutine;
        private WaitForSeconds _waitForDuration;

        public override void Initialize()
        {
            BossAttackAnimationTrigger = Animator.StringToHash("ShieldAttack");
            
            _waitForDuration = new WaitForSeconds(_duration);
            
            Disable();
        }

        public override void Disable()
        {
            if (_timerCoroutine != null)
            {
                StopCoroutine(_timerCoroutine);
                
                _timerCoroutine = null;
            }
            
            _shieldCollider.enabled = false;
            _skillCollisionHandler.enabled = false;
            _enemyCollisionHandler.enabled = true;
            
            _invincibilityCollisionHandler.enabled = false;
            View.gameObject.SetActive(false);
        }

        protected override IEnumerator Attack()
        {
            _skillCollisionHandler.ContactLimitExpired += Stun;
            
            Activate();
            
            View.gameObject.SetActive(true);

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
            _enemyCollisionHandler.enabled = false;

            _timerCoroutine = StartCoroutine(DisableTimer());
        }

        private IEnumerator DisableTimer()
        {
            yield return _waitForDuration;
            
            Disable();
        }
    }
}