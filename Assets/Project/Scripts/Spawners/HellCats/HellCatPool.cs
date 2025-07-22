using Project.Scripts.SkillSystem.SkillViews;
using UnityEngine;

namespace Project.Scripts.Spawners.HellCats
{
    public class HellCatPool : Pool<HellCat>
    {
        public HellCatPool(HellCat prefab, int startAmount) 
            : base(prefab, startAmount) { }

        protected override HellCat Create()
        {
            HellCat hellCat = Object.Instantiate(Prefab);
            hellCat.gameObject.SetActive(false);
        
            return hellCat;
        }
    }
}