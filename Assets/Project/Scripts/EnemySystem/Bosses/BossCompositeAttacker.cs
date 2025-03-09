using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.Utilities;
using UnityEngine;

namespace Project.Scripts.EnemySystem.Bosses
{
    public class BossCompositeAttacker : EnemyAttacker
    {
        [SerializeField] private List<BossAttack> _oncePerformingAttacks;
        [SerializeField] private List<BossTimedAttack> _timePerformingAttacks;

        private readonly Queue<BossAttack> _attacksOrder = new();
        private BossAttack _currentAttack;
        
        private Coroutine _performingOnceAttacks;
        private Coroutine _performingTimeAttacks;

        private void OnDisable()
        {
            if (_performingOnceAttacks != null)
                StopCoroutine(_performingOnceAttacks);
            
            if (_performingTimeAttacks != null)
                StopCoroutine(_performingTimeAttacks);
        }

        public override void Initialize(EnemyTargetProvider enemyTargetProvider)
        {
            if(_oncePerformingAttacks.IsNullOrEmpty() == false)
                _performingOnceAttacks = StartCoroutine(PerformingOnceAttacks());
            
            if(_timePerformingAttacks.IsNullOrEmpty() == false)
                _performingTimeAttacks = StartCoroutine(PerformingTimeAttacks());
            
            base.Initialize(enemyTargetProvider);
            
            List<BossAttack> allAttacks = _oncePerformingAttacks.Concat(_timePerformingAttacks).ToList();
            ApplyAttack(allAttacks[Random.Range(0, allAttacks.Count)]);
        }
        
        protected override IEnumerator Attack()
        {
            yield return _currentAttack.Performing();
            
            ApplyAttack(_attacksOrder.Dequeue());
        }

        private void ApplyAttack(BossAttack attack)
        {
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
    }
}