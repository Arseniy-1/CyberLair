using UnityEngine;

namespace Project.Scripts.Weapon.ActiveSkills.Vampirism
{
    public class HealthSpherePool : Pool<HealthSphere>
    {
        public HealthSpherePool(HealthSphere prefab, int startAmount) : base(prefab, startAmount)
        {
            
        }

        protected override HealthSphere Create()
        {
            var healthSphere =  Object.Instantiate(Prefab);
            Stack.Push(healthSphere);

            return healthSphere;
        }
    }
}