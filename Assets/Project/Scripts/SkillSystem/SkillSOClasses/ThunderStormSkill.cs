using UnityEngine;

namespace Project.Scripts.SkillSystem.SkillSOClasses
{
    [CreateAssetMenu(fileName = "ThunderStormSkill", menuName = "Skill/Mutant/ThunderStorm", order = 51)]
    public class ThunderStormSkill : MutantSkill
    {
        [field: SerializeField] [field: Range(0f, 1f)] public float Chance { get; private set; }
    }
}