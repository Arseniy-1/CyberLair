using Project.Scripts.SkillSystem.SkillViews.SummonSystem;
using UnityEngine;

namespace Project.Scripts.SkillSystem.SkillSOClasses
{
    [CreateAssetMenu(fileName = "SummonSkill", menuName = "Skill/Mutant/Summon", order = 51)]
    public class SummonSkill : MutantSkill
    {
        [field: SerializeField] public Summon SummonPrefab { get; private set; }
    }
}