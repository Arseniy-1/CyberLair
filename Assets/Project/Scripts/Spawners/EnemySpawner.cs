using System.Collections.Generic;
using Project.Scripts.CompositionRoot;
using Project.Scripts.EnemySystem;
using UnityEngine;

public class EnemySpawner : Spawner<Enemy>
{
    private List<Transform> _spawnPoints;
    
    public EnemyTypes EnemyType => Prefab.EnemyType;

    public void Initialize(Player player)
    {
        var fabric = new EnemyFabric();
        fabric.Initialize(player);
        
        Pool = new EnemyPool(Prefab, transform, StartAmount, fabric);
    }
}