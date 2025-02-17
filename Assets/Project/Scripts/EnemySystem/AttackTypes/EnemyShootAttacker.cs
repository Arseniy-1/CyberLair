using UnityEngine;

namespace Project.Scripts.EnemySystem.AttackTypes
{
    public class EnemyShootAttacker : EnemyAttacker
    {
        [SerializeField] private WeaponHolder _holder;
        [SerializeField] private EnemyWeaponStats _weaponStats;

        private void FixedUpdate()
        {
            _holder.SpotTarget(EnemyTargetProvider.Player);
        }

        public override void Initialize(EnemyTargetProvider enemyTargetProvider, IAttackerStats stats)
        {
            _holder.Weapon.Initialize(_weaponStats);
            base.Initialize(enemyTargetProvider, stats);
        }

        protected override void Attack()
        {
            _holder.Shoot();
        }
    }
}