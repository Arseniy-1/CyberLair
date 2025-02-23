using UnityEngine;

namespace Project.Prefabs.Configs.Skills.JumpSwirl
{
    [CreateAssetMenu(fileName = "JumpSwirlSkill", menuName = "Skill/Hard/JumpSwirl", order = 51)]
    public class JumpSwirlSkill : HardSkill
    {
        [field: SerializeField] public StatModifier JumpDistanceModifier {get; private set;}
        [field: SerializeField] public StatModifier MagnetRangeModifier {get; private set;}
    }
}