using Project.Scripts.Interfaces;
using Project.Scripts.SkillSystem.SkillViews;
using UnityEngine;

namespace Project.Scripts.SkillSystem.SkillSOClasses
{
    [CreateAssetMenu(fileName = "PhantomArrowsSkill", menuName = "Skill/Hard/PhantomArrows", order = 51)]
    public class PhantomArrowsSkill : HardSkill, IMagicArrowStats
    {
        [field: SerializeField] public float Radius { get; private set; }
        [field: SerializeField] public float Delay { get; private set; }
        [field: SerializeField] public LayerMask LayerMask { get; private set; }
        [field: SerializeField] public MagicArrow MagicArrowPrefab { get; private set; }
    }
}