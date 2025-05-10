using Project.Scripts.Weapon.ActiveSkills;
using UnityEngine;

namespace Project.Prefabs.Configs.Skills.InternalVoltage
{
    [CreateAssetMenu(fileName = "InternalVoltageSkill", menuName = "Skill/Hard/InternalVoltage", order = 51)]
    public class InternalVoltageSkill : HardSkill
    {
        [field: SerializeField] public float ActionRadius { get; private set; }
        [field: SerializeField] public float StunTime { get; private set; }
        [field: SerializeField] public LayerMask LayerMask { get; private set; }
        [field: SerializeField, Range(0f, 1f)] public float Chance { get; private set; }
        [field: SerializeField] public CommonSkillView SkillView { get; private set; }
    }
}