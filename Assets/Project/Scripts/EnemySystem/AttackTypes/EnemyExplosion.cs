using Project.Scripts.Services;
using UnityEngine;

namespace Project.Scripts.EnemySystem.AttackTypes
{
    public class EnemyExplosion : AttackEndHandler
    {
        [SerializeField] private Enemy _enemy;
        [SerializeField] private Explosion _explosion;

        protected override void EndAttack()
        {
            _explosion.Explode(transform.position);
            
            _enemy.Die();
        }
    }
}