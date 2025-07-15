using Project.Scripts.Interfaces;
using Project.Scripts.Skill;
using UnityEngine;

namespace Project.Prefabs.Configs.Skills.PhantomArrows
{
    [CreateAssetMenu(fileName = "PhantomArrowsSkill", menuName = "Skill/Hard/PhantomArrows", order = 51)]
    public class PhantomArrowsSkill : HardSkill, IMagicArrowStats
    {
        [field: SerializeField]public float Radius { get; private set; }
        [field: SerializeField]public float Delay { get; private set; }
        [field: SerializeField]public LayerMask LayerMask { get; private set; }
        [field: SerializeField]public MagicArrow.MagicArrow MagicArrowPrefab { get; private set; }
    }
}