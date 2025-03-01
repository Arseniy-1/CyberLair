using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
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
            
            if (_timePerformingAttacks != null)
                StopCoroutine(_performingTimeAttacks);
        }

        public override void Initialize(EnemyTargetProvider enemyTargetProvider)
        {
            if(_oncePerformingAttacks.IsNullOrEmpty() == false)
                _performingOnceAttacks = StartCoroutine(PerformingOnceAttacks());
            
            if(_timePerformingAttacks.IsNullOrEmpty() == false)
                _performingTimeAttacks = StartCoroutine(PerformingTimeAttacks());
        }
        
        protected override IEnumerator Attack()
        {
            _currentAttack.AttackPerformed += HandleAttackPerformed;
            yield return _currentAttack.Attack();
        }

        private void HandleAttackPerformed()
        {
            _currentAttack.AttackPerformed -= HandleAttackPerformed;
            _currentAttack.Disable();
            
            _currentAttack = _attacksOrder.Dequeue();
            EnemyTargetProvider.Initialize(EnemyTargetProvider.Player, _currentAttack.Range);
        }

        private IEnumerator PerformingOnceAttacks()
        {
            while (isActiveAndEnabled)
            {
                foreach (BossAttack attack in _oncePerformingAttacks)
                {
                    _attacksOrder.Enqueue(attack);

                    yield return WaitForEvent(attack);
                }
            }
        }
        
        private IEnumerator PerformingTimeAttacks()
        {
            while (isActiveAndEnabled)
            {
                foreach (BossTimedAttack attack in _timePerformingAttacks)
                {
                    yield return new WaitForSeconds(attack.Time);
                    
                    _attacksOrder.Enqueue(attack);

                    yield return WaitForEvent(attack);
                }
            }
        }
        
        private async UniTaskVoid WaitForEvent(BossAttack attack)
        {
            var tcs = new TaskCompletionSource<bool>();

            attack.AttackPerformed += OnAttack;

            await tcs.Task;
            return;

            void OnAttack()
            {
                attack.AttackPerformed -= OnAttack;
                tcs.SetResult(true);
            }
        }
    }
}