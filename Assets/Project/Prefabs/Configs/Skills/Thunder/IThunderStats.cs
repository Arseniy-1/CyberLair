using UnityEngine;

public interface IThunderStats
{
    float ActionRadius { get; }
    LayerMask LayerMask { get; }
    int Damage { get; }
    float StrikesCount { get; }
    float ShootsNeeded { get; }
}