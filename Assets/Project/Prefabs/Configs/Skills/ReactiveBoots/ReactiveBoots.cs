using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill/Passive/ReactiveBoots", order = 51)]
public class ReactiveBoots : Skill
{
    [SerializeField] private SkillConfig _speedConfig;
    [SerializeField] private SkillConfig _jumpRealoadTimeConfig;
    [SerializeField] private SkillConfig _jumpTimeConfig;

    public override void Apply(SkillData skillData)
    {
        skillData.PlayerStats.SetSpeed(skillData.StartPlayerStats.Speed *
                                       _speedConfig.Multipliers[skillData.Level - 1]);


        skillData.PlayerStats.SetJumpRealoadTime(skillData.StartPlayerStats.JumpReloadTime *
                                                 _jumpRealoadTimeConfig.Multipliers[skillData.Level - 1]);
        
        skillData.PlayerStats.SetJumpTime(skillData.StartPlayerStats.JumpTime *
                                          _jumpTimeConfig.Multipliers[skillData.Level - 1]);
    }
}