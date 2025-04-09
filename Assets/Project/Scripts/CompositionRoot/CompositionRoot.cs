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
        [SerializeField] private Level _level;
        
        [SerializeField] private StatsBar _HealthBar;
        [SerializeField] private StatsText _HealthText;
        
        [SerializeField] private StatsBar _experienceBar;
        [SerializeField] private StatsText _experienceText;
        
        private void Awake()
        {
            _edgeSpawner.SpawnOnEdges();
            
            _mainEnemySpawner.Initialize(_player);
            var waves = new Queue<Wave>(_arena.WavesConfigs
                .Select(config => new Wave(config, _mainEnemySpawner, _edgeSpawner.EdgeObjects.ToList())).ToList());

            _arena.Initialize(waves, _player.transform);
            _arena.Work();
            
            _level.Initialize(_player.ExperienceStorage);
            
            _HealthBar.Initialize(_player.PlayerStats.Health);
            _HealthText.Initialize(_player.PlayerStats.Health);
            
            _experienceBar.Initialize(_player.ExperienceStorage);
            _experienceText.Initialize(_player.ExperienceStorage);
        }
    }
}