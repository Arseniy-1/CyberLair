using Project.Scripts.Interfaces;
using Project.Scripts.SkillSystem.SkillSOClasses;
using Project.Scripts.Stats;

namespace Project.Scripts.SkillSystem.SkillInstances
{
    public class BerserkHealthRegenerator : ISkillInstance
    {
        private readonly float _criticalHealthLevel;
        private readonly StatModifier _healthRegeneratorModifier;
        private readonly HealthRegenerateAmount _healthRegenerateAmount;
    
        private readonly SkillData _skillData;
    
        public BerserkHealthRegenerator(SkillData skillData, BerserkRageSkill skill)
        {
            _criticalHealthLevel = skill.CriticalHealthLevel;
            _healthRegeneratorModifier = skill.HealthRegeneratorModifier.Copy();
            _skillData = skillData;
            _healthRegenerateAmount = _skillData.PlayerStats.HealthRegenerateAmount;
        
            _skillData.PlayerStats.Health.AmountChanged += OnHealthChanged;
        }
    
        public void Disable()
        {
            _skillData.PlayerStats.Health.AmountChanged -= OnHealthChanged;
        }
    
        private void OnHealthChanged(float maxHealth, float currentHealth)
        {
            if (currentHealth / maxHealth <= _criticalHealthLevel)
                _healthRegenerateAmount.AddModifier(_healthRegeneratorModifier);
            else
                _healthRegenerateAmount.RemoveModifier(_healthRegeneratorModifier);
        }
    }
}