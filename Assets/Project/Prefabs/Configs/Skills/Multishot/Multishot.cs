public class Multishot : ISkillInstance
{
    private readonly SkillData _data;
    private readonly MultishotSkill _multishotSkill;
        
    public Multishot(SkillData data, MultishotSkill multishotSkill)
    {
        _data = data;
        _multishotSkill = multishotSkill;
            
        _data.PlayerStats.BulletPerShootCount.AddModifier(_multishotSkill.BulletsPerShootModifier);
    }

    public  void Disable()
    {
        _data.PlayerStats.BulletPerShootCount.RemoveModifier(_multishotSkill.BulletsPerShootModifier);
    }
}