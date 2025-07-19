using Project.Scripts.SkillSystem.SkillViews;
using UnityEngine;

namespace Project.Scripts.SkillSystem.SkillSOClasses
{
    [CreateAssetMenu(fileName = "NonStopSkill", menuName = "Skill/Mutant/NonStop", order = 51)]
    public class NonStopSkill : MutantSkill
    {
        [field: SerializeField] public LandMine LandMinePrefab { get; private set; }
        [field: SerializeField] public int NeededDiedEnemyCount { get; private set; }
    }
}