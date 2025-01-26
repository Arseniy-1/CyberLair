using Project.Scripts.CompositionRoot;
using Project.Scripts.EnemySystem;
using UnityEngine;

public class EnemyPool : Pool<Enemy>
{
    private readonly EnemyFabric _enemyFabric;

    public EnemyPool(Enemy prefab, EnemyFabric enemyFabric, int startAmount) : base(prefab, startAmount)
    {
        _enemyFabric = enemyFabric;
        CreateStartCount();
    }

    protected override Enemy Create()
    {
        var enemy = _enemyFabric.Create(Prefab);

        enemy.gameObject.SetActive(false);
        // Stack.Push(enemy);

        return enemy;
    }
}