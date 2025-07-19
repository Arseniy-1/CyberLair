using Project.Scripts.Stats;
using UnityEngine;

namespace Project.Scripts.SkillSystem.SkillSOClasses
{
    [CreateAssetMenu(fileName = "HardingSkill", menuName = "Skill/Simple/Harding", order = 51)]
    public class HardingSkill : Skill
    {
        [field: SerializeField] public StatModifier JumpReloadTimeModifier { get; private set; }
    }
}