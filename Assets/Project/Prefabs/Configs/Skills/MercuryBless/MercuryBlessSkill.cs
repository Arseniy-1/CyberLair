using Project.Scripts.Skill;
using Project.Scripts.Stats;
using UnityEngine;

namespace Project.Prefabs.Configs.Skills.MercuryBless
{
    [CreateAssetMenu(fileName = "MercuryBlessSkill", menuName = "Skill/Mutant/MercuryBless", order = 51)]
    public class MercuryBlessSkill : MutantSkill
    {
        [field: SerializeField] public StatModifier DamageModifier { get; private set; }
        [field: SerializeField] public StatModifier SpeedModifier { get; private set; }
    }
}