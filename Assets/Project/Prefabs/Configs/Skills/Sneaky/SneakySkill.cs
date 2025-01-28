using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill/Passive/SneakySkill", order = 51)]
public class SneakySkill : Skill
{
    [SerializeField] private SkillConfig _skillConfig;
    
    [SerializeField] private Invulnerability _invulnerabilityPrefab;
    
    public override void Apply(SkillData skillData)
    {
        skillData.PlayerStats.SetJumpDistance((skillData.StartPlayerStats.JumpDistance *
                                               _skillConfig.Multipliers[skillData.Level]) - skillData.StartPlayerStats.JumpDistance);

        if (skillData.Level == MaxLevel)
        {
            Instantiate(_invulnerabilityPrefab, skillData.WeaponHolder.transform);
        }
    }
}