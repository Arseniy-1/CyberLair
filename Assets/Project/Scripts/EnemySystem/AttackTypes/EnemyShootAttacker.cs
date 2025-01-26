using UnityEngine;

namespace Project.Scripts.EnemySystem.AttackTypes
{
    public class EnemyShootAttacker : EnemyAttacker
    {
        [SerializeField] private WeaponHolder _gun;

        private void FixedUpdate()
        {
            _gun.SpotTarget(EnemyTargetProvider.Player);
        }

        protected override void Attack()
        {
            _gun.Shoot();
        }
    }
}