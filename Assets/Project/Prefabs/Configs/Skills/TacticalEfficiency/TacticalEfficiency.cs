public class TacticalEfficiency : ISkillInstance
{
    private readonly SkillData _data;
    private readonly TacticalEfficiencySkill _skill;
    
    public TacticalEfficiency(SkillData skillData, TacticalEfficiencySkill tacticalEfficiency)
    {
        _data = skillData;
        _skill = tacticalEfficiency;
        
        _data.PlayerStats.Health.AddModifier(_skill.HealthModifier.Copy());
        _data.PlayerStats.WeaponDamage.AddModifier(_skill.DamageModifier.Copy());
    }
    
    public void Disable()
    {
        _data.PlayerStats.Health.RemoveModifier(_skill.HealthModifier.Copy());
        _data.PlayerStats.WeaponDamage.RemoveModifier(_skill.DamageModifier.Copy());
    }
}