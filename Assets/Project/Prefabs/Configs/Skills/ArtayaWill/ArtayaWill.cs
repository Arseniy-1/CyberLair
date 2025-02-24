public class ArtayaWill : ISkillInstance
{
    public ArtayaWill(SkillData skillData, ArtayaWillSkill skill)
    {
        skillData.PlayerStats.Health.SetMaxHealth(skill.MaxHealth);

        float newMaxShield = skillData.PlayerStats.ShieldAmount.CurrentValue * skill.ShieldMultiplier;
        skillData.PlayerStats.ShieldAmount.SetMaxShield(newMaxShield);
    }

    public void Disable()
    {
    }
}