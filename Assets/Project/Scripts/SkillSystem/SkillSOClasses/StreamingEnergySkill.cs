using Project.Scripts.SkillSystem.SkillInstances;
using UnityEngine;

namespace Project.Scripts.SkillSystem.SkillSOClasses
{
    [CreateAssetMenu(fileName = "StreamingEnergySkill", menuName = "Skill/Mutant/StreamingEnergy", order = 51)]
    public class StreamingEnergySkill : MutantSkill
    {
        [field: SerializeField] public StreamingEnergy Prefab { get; private set; }
        [field: SerializeField] [field: Range(0f, 1f)] public float Chance { get; private set; }
    }
}