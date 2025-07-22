using Project.Scripts.SkillSystem.SkillInstances;
using UnityEngine;

namespace Project.Scripts.Spawners.FireZones
{
    public class FireZonePool : Pool<FireZone>
    {
        public FireZonePool(FireZone prefab, int startAmount) 
            : base(prefab, startAmount) { }

        protected override FireZone Create()
        {
            FireZone template = Object.Instantiate(Prefab);
            template.gameObject.SetActive(false);

            return template;
        }
    }
}