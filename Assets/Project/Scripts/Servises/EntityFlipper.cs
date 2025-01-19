using UnityEngine;

namespace Project.Scripts.Servises
{
    public class EntityFlipper : Flipper
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        
        protected override void CorrectFlip()
        {
            float horizontalSpeed = _rigidbody.velocity.x;
            
            if(horizontalSpeed != 0)
                SelfTransform.localScale = horizontalSpeed > 0 ? DefaultScale : FlipScale;
        }
    }
}