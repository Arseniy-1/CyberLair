using System.Collections.Generic;
using Project.Scripts.CompositionRoot;
using Project.Scripts.EnemySystem;

public class EnemyPool : Pool<Enemy>
{
    private readonly EnemyFabric _enemyFabric;
    private readonly List<Enemy> _enemies = new();

    public EnemyPool(Enemy prefab, EnemyFabric enemyFabric, int startAmount) : base(prefab, startAmount)
    {
        _enemyFabric = enemyFabric;
        CreateStartCount();
    }

    public void ApplyModifier(StatModifier statModifier)
    {
        
        
        foreach (Enemy enemy in _enemies)
        {
           enemy.EnemyStats.Speed.AddModifier(statModifier);
           enemy.EnemyStats.Health.AddModifier(statModifier);
        }
    }
        
    protected override Enemy Create()
    {
        var enemy = _enemyFabric.Create(Prefab);

        enemy.gameObject.SetActive(false);
        _enemies.Add(enemy);

        return enemy;
    }
    
}