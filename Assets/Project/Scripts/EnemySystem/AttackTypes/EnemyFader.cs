using UnityEngine;

namespace Project.Scripts.EnemySystem.AttackTypes
{
    public class EnemyFader : AttackEndHandler
    {
        [SerializeField] private Enemy _enemy;
        
        protected override void EndAttack()
        {
            _enemy.Die();
        }
    }
}