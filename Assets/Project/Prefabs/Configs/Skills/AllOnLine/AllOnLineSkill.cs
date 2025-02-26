using UnityEngine;

namespace Project.Prefabs.Configs.Skills.AllOnLine
{
    [CreateAssetMenu(fileName = "AllOnLineSkill", menuName = "Skill/Mutant/AllOnLine", order = 51)]
    public class AllOnLineSkill : MutantSkill
    {
        [field: SerializeField] public StatModifier DamageModifier { get; private set; }
    }
}