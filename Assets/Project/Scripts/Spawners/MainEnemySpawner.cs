using System.Collections.Generic;
using Project.Scripts.EnemySystem;
using UnityEngine;

public class MainEnemySpawner : MonoBehaviour
{
    [SerializeField] private List<EnemySpawner> _enemySpawners;

    private Dictionary<EnemyTypes, EnemySpawner> _spawners = new();

    // private void Awake()
    // {
    //     foreach (EnemySpawner enemySpawner in _enemySpawners)
    //     {
    //         _spawners.Add(enemySpawner.EnemyType, enemySpawner);
    //     }
    //
    //     //var bullets =  Resources.LoadAll("Bullets"); TODO: попробовать подгрузку из папки
    // }
    
    public Enemy Spawn(EnemyTypes type)
    {
        Debug.Log($"Enemy Type is {type}");
        
        var spawner = _spawners[type];
        
        // Debug.Log($"Spawner {spawner.name} will spawn it");

        return spawner.Spawn();
    }

    public void Initialize(Player player)
    {
        foreach (EnemySpawner enemySpawner in _enemySpawners)
        {
            enemySpawner.Initialize(player);
            _spawners.Add(enemySpawner.EnemyType, enemySpawner);
        }
    }
}