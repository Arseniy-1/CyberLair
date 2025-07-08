using System;
using UnityEngine;

[Serializable]
public class EnemyStats : IMoverStats
{
    [field: SerializeField] public int Experience { get; private set; }
    [field: SerializeField] public Speed Speed { get; private set; }
    [field: SerializeField] public Health Health {get; private set; }

    public void Initialize()
    {
        Speed.CalculateCurrentValue();
        Health.CalculateCurrentValue();
    }

    public void Update()
    {
        Speed.Update();
        Health.Update();
    }
}