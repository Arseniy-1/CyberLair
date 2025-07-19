using Project.Scripts.Stats;
using UnityEngine;

namespace Project.Scripts.SkillSystem.SkillSOClasses
{
    [CreateAssetMenu(fileName = "BerserkRageSkill", menuName = "Skill/Mutant/BerserkRage", order = 51)]
    public class BerserkRageSkill : MutantSkill
    {
        [field: SerializeField] public float CriticalHealthLevel { get; private set; }
        [field: SerializeField] public StatModifier HealthRegeneratorModifier {get; private set;}
    }
}