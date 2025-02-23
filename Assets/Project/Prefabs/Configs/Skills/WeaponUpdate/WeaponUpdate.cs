public class WeaponUpdate : SkillInstance
{
    private readonly SkillData _data;
    private readonly WeaponUpdateSkill _skill;
        
    public WeaponUpdate(SkillData skillData, WeaponUpdateSkill skill)
    {
        _data = skillData;
        _skill = skill;

        _data.PlayerStats.WeaponDamage.AddModifier(_skill.DamageStatModifier.Copy());
    }

    public override void Disable()
    {
        _data.PlayerStats.WeaponDamage.RemoveModifier(_skill.DamageStatModifier.Copy());
    }
}