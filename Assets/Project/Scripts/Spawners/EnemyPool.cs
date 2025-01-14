using Project.Scripts.CompositionRoot;
using Project.Scripts.EnemySystem;
using UnityEngine;

public class EnemyPool : Pool<Enemy>
{
    private readonly EnemyFabric _enemyFabric;

    public EnemyPool(Enemy prefab, EnemyFabric enemyFabric) : base(prefab)
    {
        _enemyFabric = enemyFabric;
        CreateStartCount();
    }

    protected override Enemy Create()
    {
        var enemy =  _enemyFabric.Create(Prefab);
        Stack.Push(enemy);

        return enemy;
    }
}