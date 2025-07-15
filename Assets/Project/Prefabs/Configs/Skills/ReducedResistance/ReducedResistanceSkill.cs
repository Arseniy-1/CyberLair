using Project.Prefabs.Configs.Skills.ChainZap;
using Project.Scripts.Interfaces;
using Project.Scripts.Skill;
using Project.Scripts.Stats;
using UnityEngine;

namespace Project.Prefabs.Configs.Skills.ReducedResistance
{
    [CreateAssetMenu(fileName = "ReducedResistanceSkill", menuName = "Skill/Hard/ReducedResistance", order = 51)]
    public class ReducedResistanceSkill : HardSkill, IChainZapStats
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
