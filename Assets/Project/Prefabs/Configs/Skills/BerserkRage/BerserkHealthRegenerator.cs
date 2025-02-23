using Project.Prefabs.Configs.Skills.Durability;

public class BerserkHealthRegenerator : SkillInstance
{
    private readonly float _criticalHealthLevel;
    private readonly StatModifier _healthRegeneratorModifier;
    private readonly HealthRegenerateAmount _healthRegenerateAmount;
    
    private readonly SkillData _skillData;
    
    public BerserkHealthRegenerator(SkillData skillData, BerserkRageSkill skill)
    {
        _criticalHealthLevel = skill.CriticalHealthLevel;
        _healthRegeneratorModifier = skill.HealthRegeneratorModifier;
        _skillData = skillData;
        _healthRegenerateAmount = _skillData.PlayerStats.HealthRegenerateAmount;
        
        _skillData.PlayerStats.Health.AmountChanged += OnHealthChanged;
    }
    
    private void OnHealthChanged(float maxHealth, float currentHealth)
    {
        if (currentHealth / maxHealth <= _criticalHealthLevel)
            _healthRegenerateAmount.AddModifier(_healthRegeneratorModifier);
        else
            _healthRegenerateAmount.RemoveModifier(_healthRegeneratorModifier);
    }

    public override void Disable()
    {
        _skillData.PlayerStats.Health.AmountChanged -= OnHealthChanged;
    }
}