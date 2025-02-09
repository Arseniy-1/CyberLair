using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "SneakySkill", menuName = "Skill/Simple/SneakySkill", order = 51)]
public class SneakySkill : Skill
{
    [SerializeField] private StatModifier _jumpDistanceModifier;
    [SerializeField] private StatModifier _magnetRangeModifier;

    public override void Apply(SkillData skillData)
    {
        skillData.PlayerStats.JumpDistance.AddModifier(_jumpDistanceModifier);
        skillData.PlayerStats.MagnetRange.AddModifier(_magnetRangeModifier);
    }
}