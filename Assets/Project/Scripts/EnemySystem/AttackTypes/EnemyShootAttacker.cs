using System.Collections;
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

        public override void Initialize(EnemyTargetProvider enemyTargetProvider)
        {
            _weaponStats.WeaponSpread.CalculateCurrentValue();
            _weaponStats.WeaponDamage.CalculateCurrentValue();
            _weaponStats.BulletPerShootCount.CalculateCurrentValue();
            _weaponStats.WeaponBulletReloadTime.CalculateCurrentValue();
            
            _holder.Weapon.Initialize(_weaponStats);
            
            base.Initialize(enemyTargetProvider);
        }

        protected override IEnumerator Attack()
        {
            _holder.Shoot();
            
            yield return null;
        }
    }
}