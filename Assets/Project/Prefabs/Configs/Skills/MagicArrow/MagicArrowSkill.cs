using Project.Scripts.Interfaces;
using Project.Scripts.Skill;
using UnityEngine;

namespace Project.Prefabs.Configs.Skills.MagicArrow
{
    [CreateAssetMenu(fileName = "MagicArrowSkill", menuName = "Skill/Simple/MagicArrow", order = 51)]
    public class MagicArrowSkill : Skill, IMagicArrowStats
    {
        [field: SerializeField] public float Radius { get; private set; }
        [field: SerializeField] public float Delay { get; private set; }
        [field: SerializeField] public LayerMask LayerMask { get; private set; }
        [field:SerializeField] public MagicArrow MagicArrowPrefab { get; private set; }
    }
}