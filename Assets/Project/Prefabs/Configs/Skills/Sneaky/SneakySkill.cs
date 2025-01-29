using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill/Passive/SneakySkill", order = 51)]
public class SneakySkill : Skill
{
    [SerializeField] private SkillConfig _jumpConfig;
    [SerializeField] private SkillConfig _magnetRangeConfig;

    [SerializeField] private Invulnerability _invulnerabilityPrefab;

    public override void Apply(SkillData skillData)
    {
        skillData.PlayerStats.SetJumpDistance(skillData.StartPlayerStats.JumpDistance *
                                              _jumpConfig.Multipliers[skillData.Level - 1]);


        skillData.PlayerStats.SetMagnetRange(skillData.StartPlayerStats.MagnetRange *
                                       _magnetRangeConfig.Multipliers[skillData.Level - 1]);

        if (skillData.Level == MaxLevel)
        {
            Instantiate(_invulnerabilityPrefab, skillData.WeaponHolder.transform);
        }
    }
}