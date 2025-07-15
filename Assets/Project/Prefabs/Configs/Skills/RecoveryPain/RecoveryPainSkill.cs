using Project.Scripts.Skill;
using Project.Scripts.Stats;
using UnityEngine;

namespace Project.Prefabs.Configs.Skills.RecoveryPain
{
    [CreateAssetMenu(fileName = "RecoveryPainSkill", menuName = "Skill/Hard/RecoveryPain", order = 51)]
    public class RecoveryPainSkill : HardSkill
    {
        [field: SerializeField] public StatModifier RegenerateModifier { get; private set; }
    }
}