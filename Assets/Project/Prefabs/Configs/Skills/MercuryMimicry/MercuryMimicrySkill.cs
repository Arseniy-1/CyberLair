using UnityEngine;

namespace Project.Prefabs.Configs.Skills.MercuryMimicry
{
    [CreateAssetMenu(fileName = "MercuryMimicrySkill", menuName = "Skill/Hard/MercuryMimicry", order = 51)]
    public class MercuryMimicrySkill : HardSkill
    {
        [field: SerializeField] public StatModifier SpeedModifier { get; private set; }
    }
}