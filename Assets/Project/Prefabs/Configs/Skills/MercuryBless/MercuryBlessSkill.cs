using UnityEngine;

namespace Project.Prefabs.Configs.Skills.MercuryBless
{
    [CreateAssetMenu(fileName = "TeleportationJumpSkill", menuName = "Skill/Mutant/TeleportationJump", order = 51)]
    public class MercuryBlessSkill : MutantSkill
    {
        [field: SerializeField] public StatModifier DamageModifier { get; private set; }
        [field: SerializeField] public StatModifier SpeedModifier { get; private set; }
    }
}