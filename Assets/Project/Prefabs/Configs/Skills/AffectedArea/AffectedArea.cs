using System;
using Project.Scripts.EnemySystem;
using Project.Scripts.Services.Enum;
using Project.Scripts.Services.Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Prefabs.Configs.Skills.AffectedArea
{
    [Serializable]
    public class AffectedArea : ISkillInstance
    {
        private float _radius;
        private LayerMask _layerMask;
        private float _damageProportion;
        private float _chance;

        private ShakeID _shakeID;
        private IWeaponStats _weaponStats;
        private SkillData _skillData;
        
        public AffectedArea(SkillData skillData, AffectedAreaSkill affectedAreaSkill)
        {
            skillData.WeaponHolder.Weapon.Shot += InnerSubscribe;

            _radius = affectedAreaSkill.Radius;
            _layerMask = affectedAreaSkill.LayerMask;
            _damageProportion = affectedAreaSkill.DamageProportion;
            _chance = affectedAreaSkill.Chance;
            
            _shakeID = affectedAreaSkill.ShakeID;
            _weaponStats = skillData.WeaponHolder.Weapon.WeaponStats;
            _skillData = skillData;
        }

        private void InnerSubscribe(Bullet bullet)
        {
            bullet.OnDestroyed += Blow;
        }

        private void Blow(Bullet bullet)
        {
            bullet.OnDestroyed -= Blow;
            
            if (Random.value > _chance)
                return;

            Collider2D[] results = Physics2D.OverlapCircleAll(bullet.transform.position, _radius, _layerMask);

            foreach (Collider2D affected in results)
            {
                if (affected.TryGetComponent(out Enemy affectedEnemy))
                {
                    affectedEnemy.TakeDamage(_weaponStats.WeaponDamage.CurrentValue * _damageProportion);
                }
            }
            
            _shakeID.Shake();
        }

        public void Disable()
        {
            _skillData.WeaponHolder.Weapon.Shot -= InnerSubscribe;
        }
    }
}