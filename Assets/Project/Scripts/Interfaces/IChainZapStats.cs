using UnityEngine;

public interface IChainZapStats
{
    float ChainRadius { get; }
    int MaxBounces { get; }
    float DamageFalloff { get; }
    ChainZapView ZapView { get; }
    LayerMask EnemyLayer { get; }
    int Segments { get; }
    public float Chance { get; }
    StatModifier EnemySpeedModifier { get; }
}