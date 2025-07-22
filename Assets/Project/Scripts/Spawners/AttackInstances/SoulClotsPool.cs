using Project.Scripts.EnemySystem.Bosses.Attacks.DeathReaper;
using UnityEngine;

namespace Project.Scripts.Spawners.AttackInstances
{
    public class SoulClotsPool : Pool<SoulClot>
    {
        public SoulClotsPool(SoulClot prefab, int startAmount) 
            : base(prefab, startAmount) { }
        
        protected override SoulClot Create()
        {
            SoulClot instance = Object.Instantiate(Prefab);
            instance.gameObject.SetActive(false);
            
            return instance;
        }
    }
}