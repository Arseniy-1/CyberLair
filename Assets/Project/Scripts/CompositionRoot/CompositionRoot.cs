using System.Collections.Generic;
using System.Linq;
using Project.Scripts.ArenaSystem;
using Sirenix.Utilities;
using UnityEngine;

namespace Project.Scripts.CompositionRoot
{
    public class CompositionRoot : MonoBehaviour
    {
        [SerializeField] private List<WaveConfig> _wavesConfigs;
        [SerializeField] private List<Transform> _spawnPoints;
        [SerializeField] private Arena _arena;
        [SerializeField] private EnemyFabric _fabric;
        [SerializeField] private Player _player;
        [SerializeField] private MainEnemySpawner _mainEnemySpawner;

        private void Awake()
        {
            _fabric.Initialize(_player, _spawnPoints);
            
            if (_wavesConfigs.IsNullOrEmpty()) 
                return;
            
            var waves = new Queue<Wave>(_wavesConfigs.Select(config => new Wave(config, _fabric, _mainEnemySpawner)).ToList());

            _arena.Initialize(waves);
            _arena.Work();
        }
    }
}