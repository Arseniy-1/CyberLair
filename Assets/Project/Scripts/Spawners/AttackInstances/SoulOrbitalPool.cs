using UnityEngine;

namespace Project.Scripts.EnemySystem.Bosses.DeathReaper
{
    public class SoulOrbitalPool : Pool<SoulOrbital>
    {
        public SoulOrbitalPool(SoulOrbital prefab, int startAmount) : base(prefab, startAmount) { }
        
        protected override SoulOrbital Create()
        {
            SoulOrbital instance = Object.Instantiate(Prefab);
            instance.gameObject.SetActive(false);
            
            return instance;
        }
    }
}