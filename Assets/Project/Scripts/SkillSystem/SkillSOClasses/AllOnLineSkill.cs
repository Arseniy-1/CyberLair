using Project.Scripts.Stats;
using UnityEngine;

namespace Project.Scripts.SkillSystem.SkillSOClasses
{
    [CreateAssetMenu(fileName = "AllOnLineSkill", menuName = "Skill/Mutant/AllOnLine", order = 51)]
    public class AllOnLineSkill : MutantSkill
    {
        [field: SerializeField] public StatModifier DamageModifier { get; private set; }
    }
}