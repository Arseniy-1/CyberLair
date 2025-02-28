using System.Collections.Generic;
using System.Linq;
using Project.Scripts.ArenaSystem;
using UnityEngine;

namespace Project.Scripts.CompositionRoot
{
    public class CompositionRoot : MonoBehaviour
    {
        [SerializeField] private Arena _arena;
        [SerializeField] private Player _player;
        [SerializeField] private MainEnemySpawner _mainEnemySpawner;
        [SerializeField] private EdgeSpawner _edgeSpawner;
        
        private void Awake()
        {
            _mainEnemySpawner.Initialize(_player, _edgeSpawner.SpawnOnEdges());
            
            var waves = new Queue<Wave>(_arena.WavesConfigs
                .Select(config => new Wave(config, _mainEnemySpawner)));

            _arena.Initialize(waves);
            _arena.Work();
        }
    }
}