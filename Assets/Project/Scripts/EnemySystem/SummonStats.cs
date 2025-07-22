using System;
using Project.Scripts.Interfaces;
using Project.Scripts.Stats;
using UnityEngine;

namespace Project.Scripts.EnemySystem
{
    [Serializable]
    public class SummonStats : IWeaponStats, ISummonMoveStats
    {
        [field: SerializeField] public Speed Speed { get; private set; }
        [field: SerializeField] public float MoveRadius { get; private set; }
        [field: SerializeField] public float MoveDelay { get; private set; }
        [field: SerializeField] public WeaponSpread WeaponSpread { get; private set; }
        [field: SerializeField] public WeaponDamage WeaponDamage { get; private set; }
        [field: SerializeField] public BulletPerShootCount BulletPerShootCount { get; private set; }
        [field: SerializeField] public WeaponBulletReloadTime WeaponBulletReloadTime { get; private set; }

        public void Initialize()
        {
            Speed.CalculateCurrentValue();
            WeaponSpread.CalculateCurrentValue();
            WeaponDamage.CalculateCurrentValue();
            BulletPerShootCount.CalculateCurrentValue();
            WeaponBulletReloadTime.CalculateCurrentValue();
        }
    }
}