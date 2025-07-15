using Project.Scripts.Skill;
using Project.Scripts.Stats;
using UnityEngine;

namespace Project.Prefabs.Configs.Skills.Tireless
{
    [CreateAssetMenu(fileName = "TirelessSkill", menuName = "Skill/Hard/Tireless", order = 51)]
    public class TirelessSkill : HardSkill
    {
        [field: SerializeField] public StatModifier JumpReloadTimeModifier { get; private set; }
    }
}