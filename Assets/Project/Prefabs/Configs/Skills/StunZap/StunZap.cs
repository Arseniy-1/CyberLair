using System;
using System.Collections.Generic;
using Project.Scripts.EnemySystem;
using Project.Scripts.Interfaces;
using Project.Scripts.Services.Enum;
using Project.Scripts.Skill;
using Project.Scripts.Weapon;

namespace Project.Prefabs.Configs.Skills.StunZap
{
    [Serializable]
    public class StunZap : ISkillInstance
    {
        private readonly float _stunDuration;
        private readonly SkillData _data;
        private readonly List<Bullet> _subscribedBullets = new();
    
        public StunZap(SkillData skillData, StunZapSkill skill)
        {
            _data = skillData;
            _stunDuration = skill.StunDuration;

            _data.WeaponHolder.Weapon.Shot += InnerSubscribe;
        }
    
        public void Disable()
        {
            _data.WeaponHolder.Weapon.Shot -= InnerSubscribe;

            foreach (var bullet in _subscribedBullets)
                bullet.OnDamagableCollided -= StunEnemy;
        }
    
        private void InnerSubscribe(Bullet bullet)
        {
            if(_subscribedBullets.Contains(bullet))
                return;
        
            bullet.OnDamagableCollided += StunEnemy;
            _subscribedBullets.Add(bullet);
        }

        private void StunEnemy(IDamageable damageable)
        {
            if (damageable is Enemy enemy && Enum.IsDefined(typeof(BossTypes), (BossTypes)enemy.EnemyType))
                return;
        
            (damageable as IStunable)?.TakeStun(_stunDuration);
        }
    }
}
