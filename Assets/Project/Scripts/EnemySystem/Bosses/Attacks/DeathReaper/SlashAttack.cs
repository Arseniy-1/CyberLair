using UnityEngine;

namespace Project.Scripts.EnemySystem.Bosses.DeathReaper
{
    public class SlashAttack : ColliderAttack
    {
        public override void Initialize()
        {
            BossAttackAnimationTrigger = Animator.StringToHash("SlashAttack");
            
            Disable();
        }
    }
}