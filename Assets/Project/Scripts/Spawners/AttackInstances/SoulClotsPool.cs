using UnityEngine;

namespace Project.Scripts.EnemySystem.Bosses.DeathReaper
{
    public class SoulClotsPool : Pool<SoulClot>
    {
        public SoulClotsPool(SoulClot prefab, int startAmount) : base(prefab, startAmount) { }
        
        protected override SoulClot Create()
        {
            SoulClot instance = Object.Instantiate(Prefab);
            instance.gameObject.SetActive(false);
            
            return instance;
        }
    }
}