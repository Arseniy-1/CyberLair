using System.Collections.Generic;
using Project.Scripts.EnemySystem;
using UnityEngine;

public class MainEnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemyPrefabs;
    
    private List<EnemySpawner> _enemySpawners;

    private Dictionary<EnemyTypes, EnemySpawner> _spawners = new();
    
    public Enemy Spawn(EnemyTypes type)
    {
        var spawner = _spawners[type];
        
        return spawner.Spawn();
    }

    public void Initialize(Player player)
    {
        foreach (var enemyPrefab in _enemyPrefabs)
        {
            var enemySpawner = new EnemySpawner(enemyPrefab, player);
            _spawners.Add(enemySpawner.EnemyType, enemySpawner);
        }
    }
}