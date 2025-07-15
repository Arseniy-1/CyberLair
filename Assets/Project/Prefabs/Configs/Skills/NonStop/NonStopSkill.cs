using Project.Scripts.Skill;
using UnityEngine;

namespace Project.Prefabs.Configs.Skills.NonStop
{
    [CreateAssetMenu(fileName = "NonStopSkill", menuName = "Skill/Mutant/NonStop", order = 51)]
    public class NonStopSkill : MutantSkill
    {
        [field: SerializeField] public LandMine LandMinePrefab { get; private set; }
        [field: SerializeField] public int NeededDiedEnemyCount { get; private set; }
    }
}