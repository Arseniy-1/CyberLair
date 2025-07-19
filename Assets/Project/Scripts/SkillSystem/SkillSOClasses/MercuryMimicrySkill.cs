using Project.Scripts.Stats;
using UnityEngine;

namespace Project.Scripts.SkillSystem.SkillSOClasses
{
    [CreateAssetMenu(fileName = "MercuryMimicrySkill", menuName = "Skill/Hard/MercuryMimicry", order = 51)]
    public class MercuryMimicrySkill : HardSkill
    {
        [field: SerializeField] public StatModifier SpeedModifier { get; private set; }
    }
}