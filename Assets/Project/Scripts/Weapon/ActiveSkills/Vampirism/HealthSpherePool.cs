using UnityEngine;

namespace Project.Scripts.Weapon.ActiveSkills.Vampirism
{
    public class HealthSpherePool : Pool<HealthSphere>
    {
        public HealthSpherePool(HealthSphere prefab) : base(prefab) { }

        protected override HealthSphere Create()
        {
            var enemy =  Object.Instantiate(Prefab);
            Stack.Push(enemy);

            return enemy;
        }
    }
}