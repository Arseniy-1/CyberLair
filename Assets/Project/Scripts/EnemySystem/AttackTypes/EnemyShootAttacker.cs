using UnityEngine;

namespace Project.Scripts.EnemySystem.AttackTypes
{
    public class EnemyShootAttacker : EnemyAttacker
    {
        [SerializeField] private Weapon _gun;

        protected override void Attack()
        {
            _gun.TryAttack();
        }
    }
}