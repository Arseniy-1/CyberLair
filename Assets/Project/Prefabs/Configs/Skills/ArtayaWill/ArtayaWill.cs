public class ArtayaWill : ISkillInstance
{
    private StatModifier _zeroModifier;
    private StatModifier _heatlhModifier;
    
    public ArtayaWill(SkillData skillData, ArtayaWillSkill skill)
    {
        _zeroModifier = skill.ZeroModifier;
        _heatlhModifier = skill.HeatlhModifier;
        
        skillData.PlayerStats.Health.AddModifier(_zeroModifier);
        skillData.PlayerStats.Health.AddModifier(_heatlhModifier);

        // float newMaxShield = skillData.PlayerStats.ShieldAmount.AddModifier();
        // float newMaxShield = skillData.PlayerStats.ShieldAmount.CurrentValue * skill.ShieldMultiplier;
        // skillData.PlayerStats.ShieldAmount.SetMaxShield(newMaxShield);
    }

    public void Disable()
    {
    }
}