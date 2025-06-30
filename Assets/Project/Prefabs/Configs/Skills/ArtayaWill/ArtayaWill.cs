public class ArtayaWill : ISkillInstance
{
    private readonly StatModifier _heatlhModifier;
    private readonly StatModifier _shieldModifier;

    private readonly SkillData _data;
    
    public ArtayaWill(SkillData skillData, ArtayaWillSkill skill)
    {
        _data = skillData;

        _heatlhModifier = new StatModifier(1 / skillData.PlayerStats.Health.MaxHealth, ModifierType.Multiplicative, 0);
        _shieldModifier = new StatModifier(skillData.PlayerStats.Health.MaxHealth * skill.ShieldMultiplier, ModifierType.Additive, 0);

        _data.PlayerStats.Health.AddModifier(_heatlhModifier);
        _data.PlayerStats.ShieldAmount.AddModifier(_shieldModifier);
    }

    public void Disable()
    {
        _data.PlayerStats.Health.RemoveModifier(_heatlhModifier);
        _data.PlayerStats.ShieldAmount.RemoveModifier(_shieldModifier);
    }
}