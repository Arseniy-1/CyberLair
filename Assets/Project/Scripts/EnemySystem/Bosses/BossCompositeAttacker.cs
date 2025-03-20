using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Project.Scripts.EnemySystem.AttackTypes;
using Sirenix.Utilities;
using UnityEngine;

namespace Project.Scripts.EnemySystem.Bosses
{
    public class BossCompositeAttacker : EnemyAttacker
    {
        [SerializeField] private Animator _bossAnimator;
        [SerializeField] private AttackAnimationEvents _bossAnimationEvents;
        [SerializeField] private List<BossAttack> _oncePerformingAttacks;
        [SerializeField] private List<BossTimedAttack> _timePerformingAttacks;

        private readonly Queue<BossAttack> _attacksOrder = new();
        private BossAttack _currentAttack;
        private bool _isAttacking;
        
        private Coroutine _performingOnceAttacks;
        private Coroutine _performingTimeAttacks;
        
        public override BaseEnemyAttackStats Stats => _currentAttack.AttackStats;

        private void OnDisable()
        {
            if (_performingOnceAttacks != null)
                StopCoroutine(_performingOnceAttacks);
            
            if (_performingTimeAttacks != null)
                StopCoroutine(_performingTimeAttacks);
        }

        public override void Initialize(EnemyTargetProvider enemyTargetProvider)
        {
            List<BossAttack> allAttacks = _oncePerformingAttacks.Concat(_timePerformingAttacks).ToList();

            foreach (BossAttack bossAttack in allAttacks)
            {
                bossAttack.Initialize();
            }
            
            if(_oncePerformingAttacks.IsNullOrEmpty() == false)
                _performingOnceAttacks = StartCoroutine(PerformingOnceAttacks());
            
            if(_timePerformingAttacks.IsNullOrEmpty() == false)
                _performingTimeAttacks = StartCoroutine(PerformingTimeAttacks());
            
            base.Initialize(enemyTargetProvider);
            
            ApplyAttack(allAttacks[Random.Range(0, allAttacks.Count)]);
        }
        
        protected override IEnumerator Attack()
        {
            yield return new WaitForSeconds(_currentAttack.AttackStats.AttackDelay);
            
            _bossAnimationEvents.Attacking += HandleBossAttackEvent;
            _bossAnimator.SetTrigger(_currentAttack.BossAttackAnimationTrigger);
            
            yield return new WaitUntil(() => _isAttacking);
            
            yield return _currentAttack.Performing();
            
            ApplyAttack(_attacksOrder.Dequeue());
        }

        private void ApplyAttack(BossAttack attack)
        {
            _isAttacking = false;
            _currentAttack = attack;
            EnemyTargetProvider.Initialize(EnemyTargetProvider.Player, _currentAttack.Range);
        }

        private IEnumerator PerformingOnceAttacks()
        {
            while (isActiveAndEnabled)
            {
                foreach (BossAttack attack in _oncePerformingAttacks)
                {
                    var waitUntil = new WaitUntil(() => _attacksOrder.Contains(attack) == false);
                    yield return waitUntil;
                    
                    _attacksOrder.Enqueue(attack);
                }
            }
        }
        
        private IEnumerator PerformingTimeAttacks()
        {
            while (isActiveAndEnabled)
            {
                foreach (BossTimedAttack attack in _timePerformingAttacks)
                {
                    var waitUntil = new WaitUntil(() => _attacksOrder.Contains(attack) == false);
                    yield return waitUntil;
                    
                    var waitAttackTime = new WaitForSeconds(attack.Time);
                    yield return waitAttackTime;
                    
                    _attacksOrder.Enqueue(attack);
                }
            }
        }

        private void HandleBossAttackEvent()
        {
            _bossAnimationEvents.Attacking -= HandleBossAttackEvent;
            _isAttacking = true;
        }
    }
}