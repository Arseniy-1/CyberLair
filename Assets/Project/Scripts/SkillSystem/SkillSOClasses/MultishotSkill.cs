using Project.Scripts.Stats;
using UnityEngine;

namespace Project.Scripts.SkillSystem.SkillSOClasses
{
    [CreateAssetMenu(fileName = "MultishotSkill", menuName = "Skill/Simple/Multishot", order = 51)]
    public class MultishotSkill : Skill
    {
        [field: SerializeField] public StatModifier BulletsPerShootModifier { get; private set; }
    }
}