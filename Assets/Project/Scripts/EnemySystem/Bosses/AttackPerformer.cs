using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Project.Scripts.EnemySystem.Bosses
{
    public class AttackPerformer
    {
        private readonly Queue<BossAttack> _attacksOrder;
        private readonly List<BossAttack> _attacks;
        private  CancellationTokenSource _cancellationToken;

        public AttackPerformer(Queue<BossAttack> attacksOrder, List<BossAttack> attacks)
        {
            _attacksOrder = attacksOrder;
            _attacks = attacks;
        }

        public void Start()
        {
            _cancellationToken = new CancellationTokenSource();
            
            PerformingAttack().Forget();
        }

        public void Disable()
        {
            _cancellationToken.Cancel();
        }
        
        private async UniTask PerformingAttack()
        {
            while(_cancellationToken.IsCancellationRequested == false)
            {
                foreach (BossAttack attack in _attacks)
                {
                    await UniTask.WaitUntil(() => _attacksOrder.Contains(attack) == false, cancellationToken: _cancellationToken.Token);

                    await UniTask.Delay(TimeSpan.FromSeconds(attack.AttackStats.Cooldown), cancellationToken: _cancellationToken.Token);
                    
                    _attacksOrder.Enqueue(attack);
                }
                
                await UniTask.Yield();
            }
        }
    }
}