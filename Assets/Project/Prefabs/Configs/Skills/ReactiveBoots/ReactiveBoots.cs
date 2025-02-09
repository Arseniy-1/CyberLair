using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill/Passive/ReactiveBoots", order = 51)]
public class ReactiveBoots : Skill
{
    [SerializeField] private SkillConfig _speedConfig;
    [SerializeField] private SkillConfig _jumpRealoadTimeConfig;
    [SerializeField] private SkillConfig _jumpTimeConfig;

    public override void Apply(SkillData skillData)
    {
    }
}