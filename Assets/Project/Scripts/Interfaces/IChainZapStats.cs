using Project.Prefabs.Configs.Skills.ChainZap;
using Project.Scripts.Stats;
using UnityEngine;

namespace Project.Scripts.Interfaces
{
    public interface IChainZapStats
    {
        public float ChainRadius { get; }
        public int MaxBounces { get; }
        public float DamageFalloff { get; }
        public ChainZapView ZapView { get; }
        public LayerMask EnemyLayer { get; }
        public int Segments { get; }
        public float Chance { get; }
        public StatModifier EnemySpeedModifier { get; }
    }
}