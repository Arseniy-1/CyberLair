using Project.Scripts.SkillSystem.SkillViews;
using UnityEngine;

namespace Project.Scripts.Spawners.MagicArrows
{
    public class MagicArrowPool : Pool<MagicArrow>
    {
        public MagicArrowPool(MagicArrow prefab, int startAmount) : base(prefab, startAmount) { }
        
        protected override MagicArrow Create()
        {
            var magicArrow =  Object.Instantiate(Prefab);
            magicArrow.gameObject.SetActive(false);
            
            return magicArrow;
        }
    }
}