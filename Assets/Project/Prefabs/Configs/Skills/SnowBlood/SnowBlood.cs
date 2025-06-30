public class SnowBlood : ISkillInstance
{
    private readonly SkillData _data;
    private readonly SnowBloodSkill _skill;
    
    public SnowBlood(SkillData skillData, SnowBloodSkill snowBloodSkill)
    {
        _data = skillData;
        _skill = snowBloodSkill;
        
        _data.PlayerStats.Health.AddModifier(_skill.HealthModifier);   
        _data.PlayerStats.WeaponDamage.AddModifier(_skill.DamageModifier);   
    }
    
    public void Disable()
    {
        _data.PlayerStats.Health.RemoveModifier(_skill.HealthModifier);   
        _data.PlayerStats.WeaponDamage.RemoveModifier(_skill.DamageModifier);
    }
}