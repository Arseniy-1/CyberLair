public class ReactiveBoots : SkillInstance
{
    private SkillData _data;
    private ReactiveBootsSkill _skill;
    
    public ReactiveBoots(SkillData data, ReactiveBootsSkill skill)
    {
        _data = data;
        _skill = skill;
        
        _data.PlayerStats.Speed.AddModifier(_skill.SpeedModifier);
    } 
    
    public override void Disable()
    {
        _data.PlayerStats.Speed.RemoveModifier(_skill.SpeedModifier);
    }
}