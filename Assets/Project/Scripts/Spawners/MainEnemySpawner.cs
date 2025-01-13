using System.Collections.Generic;
using Project.Scripts.EnemySystem;
using UnityEngine;

public class MainEnemySpawner : MonoBehaviour
{
    [SerializeField] private List<EnemySpawner> _enemySpawners;

    private Dictionary<EnemyTypes, EnemySpawner> _spawners = new();

    private void Awake()
    {
        foreach (EnemySpawner enemySpawner in _enemySpawners)
        {
            _spawners[enemySpawner.EnemyType] = enemySpawner;
        }

        //var bullets =  Resources.LoadAll("Bullets"); TODO: попробовать подгрузку из папки
    }

    public void Initialize(Player player)
    {
        
    }
    
    public Enemy Spawn(EnemyTypes type)
    {
        var spawner = _spawners[type];

        return spawner.Spawn();
    }
}