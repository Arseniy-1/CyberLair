using System;
using Cysharp.Threading.Tasks;

namespace Project.Scripts.EnemySystem
{
    public class EnemyAttackCooldown
    {
        public bool IsOnCooldown { get; private set; }

        public void StartCooldown(float cooldown)
        {
            Cooldown(cooldown).Forget();
        }
        
        private async UniTaskVoid Cooldown(float cooldown)
        {
            IsOnCooldown = true;
            
            await UniTask.Delay(TimeSpan.FromSeconds(cooldown));
            
            IsOnCooldown = false;
        }
    }
}