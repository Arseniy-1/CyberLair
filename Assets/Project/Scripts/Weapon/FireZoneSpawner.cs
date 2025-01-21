public class FireZoneSpawner : Spawner<FireZone>
{
    private void Awake()
    {
        Pool = new FireZonePool(Prefab);
    }
}