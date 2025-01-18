using UnityEngine;

namespace Project.Scripts.Weapon.ActiveSkills.MagicArrow
{
    public class MagicArrowPool : Pool<MagicArrow>
    {
        public MagicArrowPool(MagicArrow prefab) : base(prefab) { }
        
        protected override MagicArrow Create()
        {
            var magicArrow =  Object.Instantiate(Prefab);
            Stack.Push(magicArrow);

            return magicArrow;
        }
    }
}