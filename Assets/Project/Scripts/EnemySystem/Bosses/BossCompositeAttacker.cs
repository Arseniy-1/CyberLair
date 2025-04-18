using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Project.Scripts.EnemySystem.AttackTypes;
using Project.Scripts.MessageBroker.CameraMessageBrokers;
using UnityEngine;

namespace Project.Scripts.EnemySystem.Bosses
{
    public class BossCompositeAttacker : EnemyAttacker
    {
        [SerializeField] private Animator _bossAnimator;
        [SerializeField] private AttackAnimationEvents _bossAnimationEvents;
        [SerializeField] private List<BossAttack> _generalAttacks;
        [SerializeField] private List<BossAttack> _specialAttacks;

        private Queue<BossAttack> _attacksOrder;
        private BossAttack _currentAttack;
        private bool _isAttacking;
        
        private AttackPerformer _generalAttacksPerformer;
        private AttackPerformer _specialAttacksPerformer;
        
        public override BaseEnemyAttackStats Stats => _currentAttack.AttackStats;

        private void OnEnable()
        {
            _generalAttacksPerformer?.Start();
            _specialAttacksPerformer?.Start();
        }
        
        private void OnDisable()
        {
            _generalAttacksPerformer.Disable();
            _specialAttacksPerformer.Disable();
            
            _currentAttack.Disable();
        }

        public override void Initialize(EnemyTargetProvider enemyTargetProvider)
        {
            List<BossAttack> allAttacks = _generalAttacks.Concat(_specialAttacks).ToList();

            foreach (BossAttack bossAttack in allAttacks)
            {
                bossAttack.Initialize();
            }

            _attacksOrder = new Queue<BossAttack>();
            _generalAttacksPerformer = new AttackPerformer(_attacksOrder, _generalAttacks);
            _specialAttacksPerformer = new AttackPerformer(_attacksOrder, _specialAttacks);
            
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