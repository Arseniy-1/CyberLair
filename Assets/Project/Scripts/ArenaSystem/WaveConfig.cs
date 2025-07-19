using System.Collections.Generic;
using Project.Scripts.EnemySystem;
using Project.Scripts.Services;
using Project.Scripts.Stats;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.Scripts.ArenaSystem
{
    [CreateAssetMenu(fileName = "New Wave Config", menuName = "Wave/Create new wave config", order = 51)]
    public class WaveConfig : SerializedScriptableObject
    {
        [SerializeField] private List<ObjectWeightPair<Enemy>> _enemiesWeights;
        [SerializeField] private Enemy _boss;
        [SerializeField] [MinMaxSlider(1, 15)] private Vector2Int _spawnClusterSize;
        [SerializeField] [Range(1, 1000)] private int _waveDuration;
        [SerializeField] [Range(0.01f, 10)] private float _spawnDuration;
        [SerializeField] private StatModifier _enemyStatModifiers;
        
        public IReadOnlyList<ObjectWeightPair<Enemy>> EnemyWeights => _enemiesWeights;
        public Enemy Boss => _boss;
        public Vector2Int SpawnClusterSize => _spawnClusterSize;
        public int WaveDuration => _waveDuration;
        public float SpawnDuration => _spawnDuration;
        public StatModifier EnemyStatModifiers => _enemyStatModifiers;
    }
}