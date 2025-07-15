using System;
using Project.Scripts.Interfaces;
using Project.Scripts.Stats;
using UnityEngine;

namespace Project.Scripts.EnemySystem.AttackTypes
{
    [Serializable]
    public class EnemyWeaponStats : IWeaponStats
    {
        [field: SerializeField] public WeaponDamage WeaponDamage { get; private set; }
        [field: SerializeField] public BulletPerShootCount BulletPerShootCount { get; private set; }
        [field: SerializeField] public WeaponSpread WeaponSpread { get; private set; }
        [field: SerializeField] public WeaponBulletReloadTime WeaponBulletReloadTime { get; private set; }
    }
}