using System.Collections.Generic;
using Project.Scripts.EnemySystem;
using UnityEngine;

public class MainEnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemyPrefabs;
    [SerializeField] private int _startPoolCount;

    private readonly Dictionary<EnemyTypes, EnemySpawner> _spawners = new();
    
    public void Initialize(Player player)
    {
        foreach (var enemyPrefab in _enemyPrefabs)
        {
            var enemySpawner = new EnemySpawner(enemyPrefab, player, _startPoolCount);
            _spawners.Add(enemySpawner.EnemyType, enemySpawner);
        }
    }
    
    public Enemy Spawn(EnemyTypes type)
    {
        var spawner = _spawners[type];
        
        return spawner.Spawn();
    }

    public void ApplyModifier(StatModifier modifier)
    {
        foreach (EnemySpawner enemySpawner in _spawners.Values)
        {
            enemySpawner.ApplyModifier(modifier);
        }
    }
}