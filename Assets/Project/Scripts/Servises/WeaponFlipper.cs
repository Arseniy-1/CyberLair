using UnityEngine;

namespace Project.Scripts.Servises
{
    public class WeaponFlipper : Flipper
    {
        [SerializeField] private Transform _weaponHolder;
        
        protected override void CorrectFlip()
        {
            SelfTransform.localScale = _weaponHolder.rotation.eulerAngles.z is > 90 and < 270 ? FlipScale : DefaultScale;
        }
    }
}