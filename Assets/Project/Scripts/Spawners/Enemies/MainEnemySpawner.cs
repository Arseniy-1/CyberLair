using System.Collections.Generic;
using System.Linq;
using Project.Scripts.EnemySystem;
using Project.Scripts.PlayerSystem;
using Project.Scripts.Services;
using Project.Scripts.Services.Enum;
using Project.Scripts.Stats;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Scripts.Spawners.Enemies
{
    public class MainEnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<Enemy> _enemyPrefabs;
        [SerializeField] private int _startPoolCount;
        [SerializeField] private float _spawnOffset;

        private IReadOnlyList<Transform> _spawnPoints;
        private List<EnemyDespawner> _despawners;

        private readonly Dictionary<EnemyTypes, EnemySpawner> _spawners = new();

        private void OnDestroy()
        {
            foreach (var despawner in _despawners)
            {
                despawner.EnemyDespawn -= MoveEnemy;
            }
        }

        public void Initialize(Player player, IReadOnlyList<Transform> spawnPoints, List<EnemyDespawner> despawners)
        {
            _spawnPoints = spawnPoints;
        
            _despawners = despawners;

            foreach (var despawner in _despawners)
            {
                despawner.EnemyDespawn += MoveEnemy;
            }

            foreach (var enemySpawner in _enemyPrefabs
                         .Select(enemyPrefab => new EnemySpawner(enemyPrefab, player, _startPoolCount)))
            {
                _spawners.Add(enemySpawner.EnemyType, enemySpawner);
            }
        }

        public Enemy Spawn(EnemyTypes type)
        {
            var spawner = _spawners[type];

            Enemy enemy = spawner.Spawn();
        
            MoveEnemy(enemy);

            return enemy;
        }

        public void ApplyModifier(StatModifier modifier)
        {
            foreach (EnemySpawner enemySpawner in _spawners.Values)
            {
                enemySpawner.ApplyModifier(modifier);
            }
        }

        private void MoveEnemy(Enemy enemy)
        {
            Vector2 spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count)].position;
        
            enemy.transform.position = Random.insideUnitCircle * _spawnOffset + spawnPoint;
        }
    }
}