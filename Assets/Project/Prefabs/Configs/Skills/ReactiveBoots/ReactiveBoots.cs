using UnityEngine;

[CreateAssetMenu(fileName = "ReactiveBootsSkill", menuName = "Skill/Simple/ReactiveBoots", order = 51)]
public class ReactiveBoots : Skill
{
    [SerializeField] private StatModifier _speedModifier;

    public override void Apply(SkillData skillData)
    {
        skillData.PlayerStats.Speed.AddModifier(_speedModifier);
    }
}