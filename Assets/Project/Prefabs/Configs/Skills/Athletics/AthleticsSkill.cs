using UnityEngine;

[CreateAssetMenu(fileName = "AthleticsSkill", menuName = "Skill/Simple/Athletics", order = 51)]
public class AthleticsSkill : Skill
{
    [SerializeField] private StatModifier _jumpDistanceModifier;
    [SerializeField] private StatModifier _magnetRangeModifier;

    public override void Apply(SkillData skillData)
    {
        skillData.PlayerStats.JumpDistance.AddModifier(_jumpDistanceModifier);
        skillData.PlayerStats.MagnetRange.AddModifier(_magnetRangeModifier);
    }
}