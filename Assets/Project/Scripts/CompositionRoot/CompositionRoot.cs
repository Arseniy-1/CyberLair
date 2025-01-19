using System;
using System.Collections.Generic;
using System.Linq;
using Project.Scripts.ArenaSystem;
using Sirenix.Utilities;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace Project.Scripts.CompositionRoot
{
    public class CompositionRoot : MonoBehaviour
    {
        [SerializeField] private List<WaveConfig> _wavesConfigs;
        [SerializeField] private List<Transform> _spawnPoints;
        [SerializeField] private Arena _arena;
        [SerializeField] private Player _player;
        [SerializeField] private MainEnemySpawner _mainEnemySpawner;

        //HeartSpanwer
        //EXPSpanwer

        private void Awake()
        {
            if (_wavesConfigs.IsNullOrEmpty())
                return;

            _mainEnemySpawner.Initialize(_player);
            var waves = new Queue<Wave>(_wavesConfigs
                .Select(config => new Wave(config, _mainEnemySpawner, _spawnPoints)).ToList());

            //EXPSpanwer.init(waves);
            //HeartSpanwer.init(waves);

            _arena.Initialize(waves);
            _arena.Work();
        }
    }
}