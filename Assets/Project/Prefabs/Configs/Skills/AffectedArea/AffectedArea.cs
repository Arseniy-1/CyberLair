using System;
using Project.Prefabs.Configs.Skills.Durability;
using Project.Scripts.EnemySystem;
using Project.Scripts.Weapon;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Prefabs.Configs.Skills.AffectedArea
{
    [Serializable]
    public class AffectedArea : SkillInstance
    {
        private float _radius;
        private LayerMask _layerMask;
        private float _damageProportion;
        private float _chance;

        private IWeaponStats _weaponStats;
        private SkillData _skillData;
        
        public AffectedArea(SkillData skillData, AffectedAreaSkill affectedAreaSkill, SkillHolder skillHolder) : base(skillHolder)
        {
            skillData.WeaponHolder.Weapon.Shooted += InnerSubscribe;

            _weaponStats = skillData.WeaponHolder.Weapon.WeaponStats;
            _radius = affectedAreaSkill.Radius;
            _layerMask = affectedAreaSkill.LayerMask;
            _damageProportion = affectedAreaSkill.DamageProportion;
            _chance = affectedAreaSkill.Chance;
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
        }

        public override void Disable()
        {
            _skillData.WeaponHolder.Weapon.Shooted -= InnerSubscribe;
        }
    }
}