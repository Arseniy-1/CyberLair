using Project.Prefabs.Configs.Skills.Thunder;
using Project.Scripts.Interfaces;
using Project.Scripts.Skill;
using UnityEngine;

namespace Project.Prefabs.Configs.Skills.Overload
{
    [CreateAssetMenu(fileName = "OverloadSkill", menuName = "Skill/Hard/Overload", order = 51)]
    public class OverloadSkill : HardSkill, IThunderStats
    {
        [field: SerializeField] public float ActionRadius { get; private set; }
        [field: SerializeField] public LayerMask LayerMask { get; private set; }
        [field: SerializeField] public int Damage { get; private set; }
        [field: SerializeField] public float StrikesCount { get; private set; }
        [field: SerializeField] public float ShootsNeeded { get; private set; }
        [field: SerializeField] public CommonSkillView CommonSkillView { get; private set; }
    }
}