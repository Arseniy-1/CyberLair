using UnityEngine;

[CreateAssetMenu(fileName = "ReactiveBootsSkill", menuName = "Skill/Simple/ReactiveBoots", order = 51)]
public class ReactiveBoots : Skill
{
    [SerializeField] private SkillConfig _speedConfig;
    [SerializeField] private SkillConfig _jumpRealoadTimeConfig;
    [SerializeField] private SkillConfig _jumpTimeConfig;

    public override void Apply(SkillData skillData)
    {
    }
}