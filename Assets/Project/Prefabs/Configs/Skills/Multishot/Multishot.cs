public class Multishot : SkillInstance
{
    private SkillData _data;
    private MultishotSkill _multishotSkill;
        
    public Multishot(SkillData data, MultishotSkill multishotSkill)
    {
        _data = data;
        _multishotSkill = multishotSkill;
            
        _data.PlayerStats.BulletPerShootCount.AddModifier(_multishotSkill.BulletsPerShootModifier);
    }

    public override void Disable()
    {
        _data.PlayerStats.BulletPerShootCount.RemoveModifier(_multishotSkill.BulletsPerShootModifier);
    }
}