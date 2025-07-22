using Project.Scripts.Props;
using UnityEngine;

namespace Project.Scripts.Spawners.Health
{
    public class HealthPool : Pool<HealingHeart>
    {
        public HealthPool(HealingHeart prefab, int startAmount) 
            : base(prefab, startAmount) { }
        
        protected override HealingHeart Create()
        {
            HealingHeart health = Object.Instantiate(Prefab);
            health.gameObject.SetActive(false);
        
            return health;
        }
    }
}