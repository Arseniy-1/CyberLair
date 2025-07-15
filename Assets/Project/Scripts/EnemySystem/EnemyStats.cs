using System;
using Project.Scripts.Interfaces;
using Project.Scripts.Stats;
using UnityEngine;

namespace Project.Scripts.EnemySystem
{
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
}