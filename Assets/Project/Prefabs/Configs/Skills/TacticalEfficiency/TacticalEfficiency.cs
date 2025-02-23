public class TacticalEfficiency : SkillInstance
{
    private SkillData _data;
    private TacticalEfficiencySkill _skill;
    
    public TacticalEfficiency(SkillData skillData, TacticalEfficiencySkill tacticalEfficiency)
    {
        _data = skillData;
        _skill = tacticalEfficiency;
        
        _data.PlayerStats.Health.AddModifier(_skill.HealthModifier.Copy());
        _data.PlayerStats.WeaponDamage.AddModifier(_skill.DamageModifier.Copy());
    }
    
    public override void Disable()
    {
        _data.PlayerStats.Health.RemoveModifier(_skill.HealthModifier.Copy());
        _data.PlayerStats.WeaponDamage.RemoveModifier(_skill.DamageModifier.Copy());
    }
}