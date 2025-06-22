using DG.Tweening;
using UnityEngine;

namespace Project.Scripts.Services
{
    public class RepetitiveMover : MonoBehaviour
    {
        [SerializeField] private float _duration;
        [SerializeField] private Vector2 _offset;

        private Transform _transform; 
        private Vector2 _startPosition;
        
        private Tween _moveTween;
        
        private void Awake()
        {
            _transform = transform;
        }

        private void OnEnable()
        {
            _startPosition = _transform.position;
            Vector2 targetPos = _startPosition + _offset;
            
            _moveTween?.Kill();

            _moveTween = transform.DOMove(targetPos, _duration).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        }

        private void OnDisable()
        {
            _moveTween?.Kill();
            
            _transform.position = _startPosition;
        }
    }
}