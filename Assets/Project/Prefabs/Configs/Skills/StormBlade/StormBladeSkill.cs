using Project.Scripts.Skill;
using UnityEngine;

namespace Project.Prefabs.Configs.Skills.StormBlade
{
    [CreateAssetMenu(fileName = "StormBladeSkill", menuName = "Skill/Hard/StormBlade", order = 51)]
    public class StormBladeSkill : HardSkill
    {
        [field: SerializeField] public float MaxRadius { get; private set; }
        [field: SerializeField] public float MinRadius { get; private set; }
        [field: SerializeField] public float ChangingSpeed { get; private set; }
    }
}