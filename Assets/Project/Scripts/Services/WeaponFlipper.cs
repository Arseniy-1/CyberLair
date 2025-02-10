using UnityEngine;

namespace Project.Scripts.Servises
{
    public class WeaponFlipper : Flipper
    {
        protected override void CorrectFlip()
        {
            SelfTransform.localScale = SelfTransform.rotation.eulerAngles.z is > 90 and < 270 ? FlipScale : DefaultScale;
        }
    }
}