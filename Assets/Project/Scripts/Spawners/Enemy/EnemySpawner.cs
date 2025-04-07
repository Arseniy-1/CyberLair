using System.Collections.Generic;
using Project.Scripts.CompositionRoot;
using Project.Scripts.EnemySystem;
using UnityEngine;

public class EnemySpawner : Spawner<Enemy>
{
    private List<Transform> _spawnPoints;

    public EnemyTypes EnemyType => Prefab.EnemyType;

    public EnemySpawner(Enemy enemyPrefab, Player player, int startCount)
    {
        StartAmount = startCount;
        var fabric = new EnemyFabric(player);

        Prefab = enemyPrefab;

        Pool = new EnemyPool(Prefab, fabric, StartAmount);
    }

    public void ApplyModifier(StatModifier statModifier)
    {
        (Pool as EnemyPool)?.ApplyModifier(statModifier);
    }
}