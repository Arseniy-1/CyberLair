using UnityEngine;

namespace Project.Prefabs.Configs.Skills.ThunderStorm
{
    [CreateAssetMenu(fileName = "ThunderStormSkill", menuName = "Skill/Mutant/ThunderStorm", order = 51)]
    public class ThunderStormSkill : MutantSkill
    {
        [field: SerializeField, Range(0f,1f)] public float Chance { get; private set; }
    }
}