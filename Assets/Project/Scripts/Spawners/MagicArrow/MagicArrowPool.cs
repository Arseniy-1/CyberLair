using UnityEngine;

namespace Project.Scripts.Weapon.ActiveSkills.MagicArrow
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