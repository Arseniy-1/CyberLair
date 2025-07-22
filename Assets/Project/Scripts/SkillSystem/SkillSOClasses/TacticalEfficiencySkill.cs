using Project.Scripts.Stats;
using UnityEngine;

namespace Project.Scripts.SkillSystem.SkillSOClasses
{
    [CreateAssetMenu(fileName = "TacticalEfficiencySkill", menuName = "Skill/Simple/TacticalEfficiency", order = 51)]
    public class TacticalEfficiencySkill : Skill
    {
        [field: SerializeField] public StatModifier HealthModifier { get; private set; }
        [field: SerializeField] public StatModifier DamageModifier { get; private set; }
    }
}