using System;
using System.Collections.Generic;
using System.Linq;
using Project.Scripts.CompositionRoot;
using Project.Scripts.EnemySystem;
using UnityEngine;
using Random = UnityEngine.Random;

public class MainEnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemyPrefabs;
    [SerializeField] private int _startPoolCount;
    [SerializeField] private EnemyDespawner _enemyDespawner;
    
    [SerializeField] private List<EnemySpawner> _enemySpawners;

    private readonly Dictionary<EnemyTypes, EnemySpawner> _spawners = new();
    private List<Transform> _spawnPoints;

    private void OnDisable()
    {
        _enemyDespawner.EnemyDespawnNeeded -= MoveEnemy;
    }

    public void Initialize(Player player, List<Transform> spawnPoints)
    {
        _spawnPoints = spawnPoints;
        
        foreach (var enemySpawner in _enemyPrefabs.Select(enemyPrefab => new EnemySpawner(enemyPrefab, player, _startPoolCount)))
        {
            _spawners.Add(enemySpawner.EnemyType, enemySpawner);
        }
        
        _enemyDespawner.EnemyDespawnNeeded += MoveEnemy;
    }
    
    public Enemy Spawn(EnemyTypes type)
    {
        var spawner = _spawners[type];
        var enemy = spawner.Spawn();
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
        enemy.Rigidbody2D.position = _spawnPoints[Random.Range(0, _spawnPoints.Count)].position;
    }
}