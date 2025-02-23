using System;
using Project.Prefabs.Configs.Skills.Durability;
using UnityEngine;

namespace Project.Prefabs.Configs.Skills
{
    public class FirstAid : ISkillInstance
    {
        private readonly Health _health;
        private float _healProportion;
        
        public FirstAid(SkillData skillData, FirstAidSkill skill)
        {
            _health = skillData.PlayerStats.Health;
            _healProportion = skill.HealProportion;
            _health.DamageTaken += HealPart;
        }

        private void HealPart(float damage)
        {
            if (damage < 0)
                return;
            
            _health.Heal(damage * _healProportion);
        }

        public void Disable()
        {
            _health.DamageTaken -= HealPart;
        }
    }
}