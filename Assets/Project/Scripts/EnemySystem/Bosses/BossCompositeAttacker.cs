using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Project.Scripts.EnemySystem.AttackTypes;
using UnityEngine;

namespace Project.Scripts.EnemySystem.Bosses
{
    public class BossCompositeAttacker : EnemyAttacker
    {
        [SerializeField] private Animator _bossAnimator;
        [SerializeField] private AttackAnimationEvents _bossAnimationEvents;
        [SerializeField] private List<BossAttack> _oncePerformingAttacks;
        [SerializeField] private List<BossAttack> _timePerformingAttacks;

        private readonly Queue<BossAttack> _attacksOrder = new();
        private BossAttack _currentAttack;
        private bool _isAttacking;
        
        private AttackPerformer _onceAttacksPerformer;
        private AttackPerformer _timeAttacksPerformer;
        
        public override BaseEnemyAttackStats Stats => _currentAttack.AttackStats;

        private void OnDisable()
        {
            _onceAttacksPerformer.Disable();
            _timeAttacksPerformer.Disable();
        }

        public override void Initialize(EnemyTargetProvider enemyTargetProvider)
        {
            List<BossAttack> allAttacks = _oncePerformingAttacks.Concat(_timePerformingAttacks).ToList();

            foreach (BossAttack bossAttack in allAttacks)
            {
                bossAttack.Initialize();
            }

            _onceAttacksPerformer = new AttackPerformer(_attacksOrder, _oncePerformingAttacks);
            _timeAttacksPerformer = new AttackPerformer(_attacksOrder, _timePerformingAttacks);
            
            _onceAttacksPerformer.Start();
            _timeAttacksPerformer.Start();
            
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

        private void HandleBossAttackEvent()
        {
            _bossAnimationEvents.Attacking -= HandleBossAttackEvent;
            _isAttacking = true;
        }
    }
}