using UnityEngine;

namespace Project.Prefabs.Configs.Skills.StreamingEnergy
{
    [CreateAssetMenu(fileName = "ThunderStormSkill", menuName = "Skill/Mutant/ThunderStorm", order = 51)]
    public class StreamingEnergySkill : MutantSkill
    {
        [field: SerializeField] public StreamingEnergy Prefab { get; private set; }
        [field: SerializeField, Range(0f, 1f)] public float Chance { get; private set; }
    }
}