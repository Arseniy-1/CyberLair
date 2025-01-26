using System.Collections.Generic;
using Project.Scripts.EnemySystem;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project.Scripts.ArenaSystem
{
    [CreateAssetMenu(fileName = "New Wave Config", menuName = "Wave/Create new wave config", order = 51)]
    public class WaveConfig : SerializedScriptableObject
    {
        [OdinSerialize] private Dictionary<Enemy,int> _enemies;
        [SerializeField, Range(1, 1000)] private int _waveDuration;
        [SerializeField,Range(0.01f, 10)] private float _spawnDuration;
        
        public IReadOnlyDictionary<Enemy,int> Enemies => _enemies;
        public int WaveDuration => _waveDuration;
        public float SpawnDuration => _spawnDuration;
    }
}