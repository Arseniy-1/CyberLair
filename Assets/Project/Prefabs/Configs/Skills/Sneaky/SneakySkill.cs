using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill/Passive/SneakySkill", order = 51)]
public class SneakySkill : Skill
{
    [SerializeField] private SkillConfig _jumpSkillConfig;
    [SerializeField] private SkillConfig _speedSkillConfig;
    
    [SerializeField] private Invulnerability _invulnerabilityPrefab;
    
    public override void Apply(SkillData skillData)
    {
        skillData.PlayerStats.SetJumpDistance(skillData.StartPlayerStats.JumpDistance *
                                               _jumpSkillConfig.Multipliers[skillData.Level - 1]);
        
        
        skillData.PlayerStats.SetSpeed(skillData.StartPlayerStats.Speed *
                                        _speedSkillConfig.Multipliers[skillData.Level - 1]);
        
        if (skillData.Level == MaxLevel)
        {
            Instantiate(_invulnerabilityPrefab, skillData.WeaponHolder.transform);
        }
    }
}