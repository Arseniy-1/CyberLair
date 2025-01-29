using System;
using UnityEngine;

namespace Project.Scripts.EnemySystem.AttackTypes
{
    [Serializable]
    public class EnemyWeaponStats : IWeaponStats
    {
        [field: SerializeField] public int WeaponDamage { get; private set; }
        [field: SerializeField] public float WeaponSpread { get; private set; }
        [field: SerializeField] public float WeaponBulletReloadTime { get; private set; }
    }
}