using System.Collections.Generic;
using System.Linq;
using Project.Scripts.EnemySystem;
using UnityEngine;

public class MainEnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemyPrefabs;
    [SerializeField] private int _startPoolCount;
    
    private List<EnemySpawner> _enemySpawners;

    private readonly Dictionary<EnemyTypes, EnemySpawner> _spawners = new();
    
    public void Initialize(Player player)
    {
        foreach (var enemySpawner in _enemyPrefabs.Select(enemyPrefab => new EnemySpawner(enemyPrefab, player, _startPoolCount)))
        {
            _spawners.Add(enemySpawner.EnemyType, enemySpawner);
        }
    }
    
    public Enemy Spawn(EnemyTypes type)
    {
        // EnemySpawner spawner;
        //
        // if (type == EnemyTypes.Boss)
        // {
        //     List<EnemySpawner> bossSpawners =
        //         _spawners.Values.Where(enemySpawner => enemySpawner.EnemyType == type).ToList();
        //     
        //     spawner = bossSpawners.OrderBy(_ => Random.value).First();
        //     
        // }
        
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