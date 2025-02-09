using UnityEngine;

[CreateAssetMenu(fileName = "ReactiveBootsSkill", menuName = "Skill/Simple/ReactiveBoots", order = 51)]
public class ReactiveBoots : Skill
{
    [SerializeField] private StatModifier _speedModifier;
    [SerializeField] private StatModifier _jumpReloadTimeModifier;

    public override void Apply(SkillData skillData)
    {
        skillData.PlayerStats.Speed.AddModifier(_speedModifier);
        skillData.PlayerStats.JumpReloadTime.AddModifier(_jumpReloadTimeModifier);
    }
}