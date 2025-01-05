using System.Collections.Generic;
using Project.Scripts.EnemySystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.Scripts.ArenaSystem
{
    [CreateAssetMenu(fileName = "New Wave Config", menuName = "Wave/Create new wave config", order = 51)]
    public class WaveConfig : SerializedScriptableObject
    {
        [SerializeField] private Dictionary<Enemy,int> _enemies;
        [SerializeField] private float _duration;
        
        public IReadOnlyDictionary<Enemy,int> Enemies => _enemies;
    }
}