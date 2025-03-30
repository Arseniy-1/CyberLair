using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace.Tween
{
    public class CameraShaker : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        
        private Vector3 _originalPosition;

        public void Shake(float duration, float strength, int vibrato, float randomness)
        {
            _originalPosition = _camera.transform.localPosition;
            
            _camera.transform.DOShakePosition(duration, strength, vibrato, randomness)
                .OnComplete(() => _camera.transform.localPosition = _originalPosition);
        }
    }
}