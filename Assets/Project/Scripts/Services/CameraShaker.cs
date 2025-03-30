using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace.Tween
{
    public class CameraShaker : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        
        private Vector3 _originalPosition;

        public void Shake(float duration = 0.5f, float strength = 0.5f, int vibrato = 10, float randomness = 90f)
        {
            _originalPosition = _camera.transform.localPosition;
            
            _camera.transform.DOShakePosition(duration, strength, vibrato, randomness)
                .OnComplete(() => _camera.transform.localPosition = _originalPosition);
        }
    }
}