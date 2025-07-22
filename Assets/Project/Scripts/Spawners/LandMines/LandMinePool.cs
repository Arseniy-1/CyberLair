using Project.Scripts.SkillSystem.SkillViews;
using UnityEngine;

namespace Project.Scripts.Spawners.LandMines
{
    public class LandMinePool : Pool<LandMine>
    {
        public LandMinePool(LandMine prefab, int startAmount) 
            : base(prefab, startAmount) { }

        protected override LandMine Create()
        {
            LandMine landMine = Object.Instantiate(Prefab);
            landMine.gameObject.SetActive(false);
        
            return landMine;
        }
    }
}