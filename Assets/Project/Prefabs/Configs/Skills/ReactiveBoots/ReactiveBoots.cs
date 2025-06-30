public class ReactiveBoots : ISkillInstance
{
    private readonly SkillData _data;
    private readonly ReactiveBootsSkill _skill;
    
    public ReactiveBoots(SkillData data, ReactiveBootsSkill skill)
    {
        _data = data;
        _skill = skill;
        
        _data.PlayerStats.Speed.AddModifier(_skill.SpeedModifier);
    } 
    
    public void Disable()
    {
        _data.PlayerStats.Speed.RemoveModifier(_skill.SpeedModifier);
    }
}