using System;
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
            _edgeSpawner.SpawnOnEdges();
            
            _mainEnemySpawner.Initialize(_player);
            var waves = new Queue<Wave>(_arena.WavesConfigs
                .Select(config => new Wave(config, _mainEnemySpawner, _edgeSpawner.EdgeObjects.ToList())).ToList());

            _arena.Initialize(waves);
            _arena.Work();
        }
    }
}