using Project.Scripts.CompositionRoot;
using Project.Scripts.EnemySystem;
using UnityEngine;

public class EnemyPool : Pool<Enemy>
{
    private EnemyFabric _enemyFabric;

    public EnemyPool(Enemy prefab, Transform container, int startAmount, EnemyFabric enemyFabric) : base(prefab, container, startAmount)
    {
        _enemyFabric = enemyFabric;
    }

    protected override Enemy Create()
    {
        return _enemyFabric.Create(Prefab);
    }
}