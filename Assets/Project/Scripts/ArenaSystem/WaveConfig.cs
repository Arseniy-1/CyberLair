using System.Collections.Generic;
using Project.Scripts.EnemySystem;
using Project.Scripts.Servises;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.Scripts.ArenaSystem
{
    [CreateAssetMenu(fileName = "New Wave Config", menuName = "Wave/Create new wave config", order = 51)]
    public class WaveConfig : SerializedScriptableObject
    {
        [SerializeField] private List<ObjectWeightPair<Enemy>> _enemiesWeights;
        [SerializeField, Range(1, 10)] private int _spawnClusterSize;
        [SerializeField, Range(1, 1000)] private int _waveDuration;
        [SerializeField,Range(0.01f, 10)] private float _spawnDuration;
        [SerializeField] private StatModifier _enemyStatModifiers;
        
        public IReadOnlyList<ObjectWeightPair<Enemy>> EnemyWeights => _enemiesWeights;
        public int SpawnClusterSize => _spawnClusterSize;
        public int WaveDuration => _waveDuration;
        public float SpawnDuration => _spawnDuration;
        public StatModifier EnemyStatModifiers => _enemyStatModifiers;
    }
}