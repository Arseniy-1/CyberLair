using Project.Scripts.CompositionRoot;
using Project.Scripts.EnemySystem;

public class EnemySpawner : Spawner<Enemy>
{
    public EnemyTypes EnemyType => Prefab.EnemyType;

    public void Initialize(Player player)
    {
        var fabric = new EnemyFabric();
        fabric.Initialize(player);
        
        Pool = new EnemyPool(Prefab, transform, StartAmount, fabric);
    }
    
    private void Awake()
    {
    }
}