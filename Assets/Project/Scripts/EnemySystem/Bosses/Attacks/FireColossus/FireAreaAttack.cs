using UnityEngine;

namespace Project.Scripts.EnemySystem.Bosses.FireColossus
{
    public class FireAreaAttack : ColliderAttack
    {
        public override void Initialize()
        {
            BossAttackAnimationTrigger = Animator.StringToHash("FireAreaAttack");
            
            Disable();
        }
    }
}