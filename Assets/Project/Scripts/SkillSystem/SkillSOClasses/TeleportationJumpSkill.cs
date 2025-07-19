using Project.Scripts.Stats;
using UnityEngine;

namespace Project.Scripts.SkillSystem.SkillSOClasses
{
    [CreateAssetMenu(fileName = "TeleportationJumpSkill", menuName = "Skill/Mutant/TeleportationJump", order = 51)]
    public class TeleportationJumpSkill : MutantSkill
    {
        [field: SerializeField] public StatModifier JumpTimeModifier { get; private set; }
        [field: SerializeField] public StatModifier JumpSpeedModifier { get; private set; }
    }
}