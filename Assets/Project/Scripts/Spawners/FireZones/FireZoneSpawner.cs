using System;
using Project.Scripts.SkillSystem.SkillInstances;

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