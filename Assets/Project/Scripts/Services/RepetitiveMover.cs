using DG.Tweening;
using UnityEngine;

namespace Project.Scripts.Services
{
    public class RepetitiveMover : MonoBehaviour
    {
        [SerializeField] private float _duration;
        [SerializeField] private Vector2 _offset;
        [SerializeField] private RectTransform _transform; 

        private Vector2 _startPosition;
        
        private Tween _moveTween;

        private void OnEnable()
        {
            DisableMove();

            _startPosition = _transform.position;
            Vector2 targetPosition = _startPosition + _offset;

            _moveTween = _transform
                .DOMove(targetPosition, _duration)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo)
                .SetUpdate(true);
        }

        private void OnDisable()
        {
            DisableMove();
            
            _transform.position = _startPosition;
        }

        private void DisableMove()
        {
            _moveTween?.Kill();
        }
    }
}