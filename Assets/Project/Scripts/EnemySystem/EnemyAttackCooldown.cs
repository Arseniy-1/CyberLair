using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Project.Scripts.EnemySystem
{
    public class EnemyAttackCooldown
    {
        private CancellationTokenSource _cancellationToken;
        
        public bool IsOnCooldown { get; private set; }

        public void StartCooldown(float cooldown)
        {
            _cancellationToken?.Cancel();
            _cancellationToken = new CancellationTokenSource();
            
            Cooldown(cooldown, _cancellationToken.Token).Forget();
        }
        
        public void EndCooldown()
        {
            _cancellationToken?.Cancel();
        }
        
        private async UniTaskVoid Cooldown(float cooldown, CancellationToken token)
        {
            IsOnCooldown = true;
            
            await UniTask.Delay(TimeSpan.FromSeconds(cooldown), cancellationToken: token);
            
            IsOnCooldown = false;
            
            EndCooldown();
        }
    }
}