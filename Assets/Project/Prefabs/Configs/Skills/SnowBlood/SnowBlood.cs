using Project.Scripts.Interfaces;
using Project.Scripts.Skill;
using Project.Scripts.Stats;

namespace Project.Prefabs.Configs.Skills.SnowBlood
{
    public class SnowBlood : ISkillInstance
    {
        private readonly SkillData _data;

        private readonly StatModifier _healthModifier;
        private readonly StatModifier _damageModifier;
    
        public SnowBlood(SkillData skillData, SnowBloodSkill snowBloodSkill)
        {
            _data = skillData;

            _healthModifier = snowBloodSkill.HealthModifier.Copy();
            _damageModifier = snowBloodSkill.DamageModifier.Copy();
        
            _data.PlayerStats.Health.AddModifier(_healthModifier);   
            _data.PlayerStats.WeaponDamage.AddModifier(_damageModifier);   
        }
    
        public void Disable()
        {
            _data.PlayerStats.Health.RemoveModifier(_healthModifier);   
            _data.PlayerStats.WeaponDamage.RemoveModifier(_damageModifier);
        }
    }
}