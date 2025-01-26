using System;

[Serializable]
public class FireZoneSpawner : Spawner<FireZone>
{
    public FireZoneSpawner(FireZone prefab)
    {
        Prefab = prefab;
        Pool = new FireZonePool(Prefab, StartAmount);
    }
}