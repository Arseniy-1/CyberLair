using Project.Scripts.Skill;
using UnityEngine;

namespace Project.Prefabs.Configs.Skills.Summon
{
    [CreateAssetMenu(fileName = "SummonSkill", menuName = "Skill/Mutant/Summon", order = 51)]
    public class SummonSkill : MutantSkill
    {
        [field: SerializeField] public Summon SummonPrefab { get; private set; }
    }
}