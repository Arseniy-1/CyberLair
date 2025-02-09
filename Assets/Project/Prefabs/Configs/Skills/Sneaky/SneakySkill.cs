using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "SneakySkill", menuName = "Skill/Simple/SneakySkill", order = 51)]
public class SneakySkill : Skill
{
    [SerializeField] private SkillConfig _jumpConfig;
    [SerializeField] private SkillConfig _magnetRangeConfig;

    [SerializeField] private Invulnerability _invulnerabilityPrefab;

    public override void Apply(SkillData skillData)
    {

    }
}