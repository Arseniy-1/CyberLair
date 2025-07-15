using Project.Scripts.Interfaces;
using Project.Scripts.Skill;
using Project.Scripts.Stats;

namespace Project.Prefabs.Configs.Skills.TacticalEfficiency
{
    public class TacticalEfficiency : ISkillInstance
    {
        private readonly SkillData _data;
        private readonly StatModifier _healthModifier;
        private readonly StatModifier _damageModifier;
    
        public TacticalEfficiency(SkillData skillData, TacticalEfficiencySkill tacticalEfficiency)
        {
            _data = skillData;

            _healthModifier = tacticalEfficiency.HealthModifier.Copy();
            _damageModifier = tacticalEfficiency.DamageModifier.Copy();
        
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