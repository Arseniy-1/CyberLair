using Project.Scripts.Stats;
using UnityEngine;

namespace Project.Scripts.SkillSystem.SkillSOClasses
{
    [CreateAssetMenu(fileName = "AthleticsSkill", menuName = "Skill/Simple/Athletics", order = 51)]
    public class AthleticsSkill : Skill
    {
        [field: SerializeField] public StatModifier JumpDistanceModifier {get; private set;}
        [field: SerializeField] public StatModifier MagnetRangeModifier {get; private set;}
    }
}