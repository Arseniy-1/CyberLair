using Project.Scripts.Interfaces;
using Project.Scripts.SkillSystem.SkillViews;
using Project.Scripts.Stats;
using UnityEngine;

namespace Project.Scripts.SkillSystem.SkillSOClasses
{
    [CreateAssetMenu(fileName = "ChainZapSkill", menuName = "Skill/Simple/ChainZap", order = 51)]
    public class ChainZapSkill : Skill, IChainZapStats
    {
        [field: SerializeField] public float ChainRadius { get; private set; }
        [field: SerializeField] public int MaxBounces { get; private set; }
        [field: SerializeField] public float DamageFalloff { get; private set; }
        [field: SerializeField] public ChainZapView ZapView { get; private set; }
        [field: SerializeField] public LayerMask EnemyLayer { get; private set; }
        [field: SerializeField] public int Segments { get; private set; }
        [field: SerializeField, Range(0f, 1f)] public float Chance { get; private set; }
        [field: SerializeField] public StatModifier EnemySpeedModifier { get; private set; }
    }
}