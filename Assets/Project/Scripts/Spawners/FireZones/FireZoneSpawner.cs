using System;
using Project.Prefabs.Configs.Skills.FireZone;

namespace Project.Scripts.Spawners.FireZones
{
    [Serializable]
    public class FireZoneSpawner : Spawner<FireZone>
    {
        public FireZoneSpawner(FireZone prefab)
        {
            Prefab = prefab;
            Pool = new FireZonePool(Prefab, StartAmount);
        }
    }
}