using UnityEngine;

namespace Project.Scripts.SkillSystem.SkillSOClasses
{
    [CreateAssetMenu(fileName = "StunZapSkill", menuName = "Skill/Simple/StunZap", order = 51)]
    public class StunZapSkill : Skill
    {
        [field: SerializeField] public float StunDuration { get; private set; }
    }
}