using Project.Scripts.Interfaces;
using Project.Scripts.Services.Enum;
using Project.Scripts.Skill;
using Project.Scripts.Stats;

namespace Project.Prefabs.Configs.Skills.ArtayaWill
{
    public class ArtayaWill : ISkillInstance
    {
        private readonly StatModifier _healthModifier;
        private readonly StatModifier _shieldModifier;

        private readonly SkillData _data;
    
        public ArtayaWill(SkillData skillData, ArtayaWillSkill skill)
        {
            _data = skillData;

            _healthModifier = new StatModifier(1 / skillData.PlayerStats.Health.MaxHealth, ModifierType.Multiplicative, 0);
            _shieldModifier = new StatModifier(skillData.PlayerStats.Health.MaxHealth * skill.ShieldMultiplier, ModifierType.Additive, 0);

            _data.PlayerStats.Health.AddModifier(_healthModifier);
            _data.PlayerStats.ShieldAmount.AddModifier(_shieldModifier);
        }

        public void Disable()
        {
            _data.PlayerStats.Health.RemoveModifier(_healthModifier);
            _data.PlayerStats.ShieldAmount.RemoveModifier(_shieldModifier);
        }
    }
}