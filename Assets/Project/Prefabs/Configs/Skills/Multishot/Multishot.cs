using Project.Prefabs.Configs.Skills.Durability;

public class Multishot : ISkillInstance
{
    private SkillData _data;
    private MultishotSkill _multishotSkill;
        
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