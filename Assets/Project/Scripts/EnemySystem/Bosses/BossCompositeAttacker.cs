using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Project.Scripts.EnemySystem.AttackTypes;
using Project.Scripts.MessageBroker.CameraMessageBrokers;
using Sirenix.Utilities;
using UnityEngine;

namespace Project.Scripts.EnemySystem.Bosses
{
    public class BossCompositeAttacker : EnemyAttacker
    {
        [SerializeField] private Animator _bossAnimator;
        [SerializeField] private AttackAnimationEvents _bossAnimationEvents;
        [SerializeField] private List<BossAttack> _generalAttacks;
        [SerializeField] private List<BossAttack> _specialAttacks;

        private readonly Queue<BossAttack> _attacksOrder = new();
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
            Debug.Log("Boss Composite Attacker is disabled");
            
            _generalAttacksPerformer?.Disable();
            _specialAttacksPerformer?.Disable();
            
            _currentAttack?.Disable();
        }

        public override void Initialize(EnemyTargetProvider enemyTargetProvider)
        {
            List<BossAttack> allAttacks = _generalAttacks.Concat(_specialAttacks).ToList();

            allAttacks.ForEach(attack => attack.Initialize());
            
            if(_generalAttacks.IsNullOrEmpty() == false)
                _generalAttacksPerformer = new AttackPerformer(_attacksOrder, _generalAttacks);
            
            if(_specialAttacks.IsNullOrEmpty() == false)
                _specialAttacksPerformer = new AttackPerformer(_attacksOrder, _specialAttacks);
            
            base.Initialize(enemyTargetProvider);
            
            _attacksOrder.Enqueue(allAttacks[Random.Range(0, allAttacks.Count)]);
        }
        
        protected override IEnumerator Attack()
        {
            // Debug.Log($"Waiting attack order = {_attacksOrder.Count > 0}");
            yield return new WaitUntil(() => _attacksOrder.Count > 0);
            // Debug.Log($"Waiting attack order = {_attacksOrder.Count > 0}");
            
            ApplyAttack(_attacksOrder.Dequeue());
            
            // Debug.Log($"{gameObject.name} delay");
            yield return new WaitForSeconds(_currentAttack.AttackStats.AttackDelay);
            
            _bossAnimationEvents.Attacking += HandleBossAttackEvent;
            _bossAnimator.SetTrigger(_currentAttack.BossAttackAnimationTrigger);

            yield return new WaitUntil(() => _isAttacking);
            
            // Debug.Log($"Waiting {_currentAttack.name}");
            
            yield return _currentAttack.Performing();
            
            // Debug.Log($"{_currentAttack.name} is done");
        }

        private void ApplyAttack(BossAttack attack)
        {
            _isAttacking = false;

            if (_currentAttack)
                _bossAnimator.ResetTrigger(_currentAttack.BossAttackAnimationTrigger);
            
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