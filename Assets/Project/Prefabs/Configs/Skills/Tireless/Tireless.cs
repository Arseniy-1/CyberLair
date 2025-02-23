public class Tireless : SkillInstance
{
    private readonly SkillData _data;
    private readonly TirelessSkill _skill;

    public Tireless(SkillData skillData, TirelessSkill skill)
    {
        _data = skillData;
        _skill = skill;

        _data.PlayerStats.WeaponDamage.AddModifier(_skill.JumpReloadTimeModifier.Copy());
    }

    public override void Disable()
    {
        _data.PlayerStats.WeaponDamage.RemoveModifier(_skill.JumpReloadTimeModifier.Copy());
    }
}