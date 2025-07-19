using System;
using System.Linq;
using Project.Scripts.Interfaces;
using Project.Scripts.Services.Enum;
using Project.Scripts.Services.Extensions;
using Project.Scripts.SkillSystem.SkillSOClasses;
using Project.Scripts.Weapon;
using Sirenix.Utilities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Scripts.SkillSystem.SkillInstances
{
    [Serializable]
    public class AffectedArea : ISkillInstance
    {
        private const int MaxHits = 12;
        
        private readonly Collider2D[] _results = new Collider2D[MaxHits];
        
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
        
        public void Disable()
        {
            _skillData.WeaponHolder.Weapon.Shot -= InnerSubscribe;
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

            int hitCount = Physics2D.OverlapCircleNonAlloc(bullet.Position, _radius, _results, _layerMask);

            _results
                .Take(hitCount)
                .ForEach(hit =>
                {
                    if(hit.TryGetComponent(out IDamageable affectedEnemy))
                        affectedEnemy.TakeDamage(_weaponStats.WeaponDamage.CurrentValue * _damageProportion);
                });
            
            _shakeID.Shake();
        }
    }
}