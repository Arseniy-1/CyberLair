using System.Collections.Generic;
using Project.Scripts.CompositionRoot;
using Project.Scripts.EnemySystem;

public class EnemyPool : Pool<Enemy>
{
    private readonly EnemyFabric _enemyFabric;
    private readonly List<Enemy> _enemies = new();
    private readonly List<StatModifier> _modifiers = new();

    public EnemyPool(Enemy prefab, EnemyFabric enemyFabric, int startAmount) : base(prefab, startAmount)
    {
        _enemyFabric = enemyFabric;
        
        CreateStartCount();
    }

    public void AddModifier(StatModifier statModifier)
    {
        _modifiers.Add(statModifier);
        
        foreach (Enemy enemy in _enemies)
        {
           ApplyModifier(enemy, statModifier);
        }
    }
        
    protected override Enemy Create()
    {
        var enemy = _enemyFabric.Create(Prefab);

        foreach (StatModifier modifier in _modifiers)
        {
            ApplyModifier(enemy, modifier);
        }

        enemy.gameObject.SetActive(false);
        _enemies.Add(enemy);

        return enemy;
    }

    private void ApplyModifier(Enemy enemy, StatModifier statModifier)
    {
        enemy.EnemyStats.Speed.AddModifier(statModifier.Copy());
        enemy.EnemyStats.Health.AddModifier(statModifier.Copy());
    }
}