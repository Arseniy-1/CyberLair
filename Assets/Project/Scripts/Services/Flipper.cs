using UnityEngine;

namespace Project.Scripts.Servises
{
    public abstract class Flipper : MonoBehaviour
    {
        [SerializeField] protected Vector3 FlipScale;
        
        protected Vector3 DefaultScale;
        protected Transform SelfTransform;

        private void Awake()
        {
            SelfTransform = transform;
            DefaultScale = SelfTransform.localScale;
        }

        private void LateUpdate()
        {
            CorrectFlip();
        }

        protected abstract void CorrectFlip();
    }
}