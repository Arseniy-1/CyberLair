using System;
using System.Collections;
using DG.Tweening;
using Project.Scripts.Services;
using UnityEngine;

namespace Project.Scripts.EnemySystem.Bosses.DeathReaper
{
    public class SoulClot : MonoBehaviour, IDestoyable<SoulClot>, IReturnable
    {
        private const string ArriveSound = "Arrive";
        
        [SerializeField] private Transform _transform;
        [SerializeField] private float _duration = 1f;
        [SerializeField] private float _arcHeight = 2f;
        [SerializeField] private int _pathResolution = 10;
        [SerializeField] private float _timeToDestroy = 1.5f;
        [SerializeField] private SoundAnimationEvents _soundEvents;

        private Vector3 _target;
        private Vector3 _previousPosition;
        private Coroutine _destroyCoroutine;
        
        private Tween _pathTween;
        private Tween _rotationTween;
        
        public event Action<SoulClot> OnDestroyed;
        
        private void OnDisable()
        {
            ReturnToPool();
        }

        public void Initialize(Vector3 target)
        {
            _target = target;
            
            _destroyCoroutine ??= StartCoroutine(WaitForDestroy());
        }
        
        public void ReturnToPool()
        {
            StopMove();
            
            OnDestroyed?.Invoke(this);
            
            if (_destroyCoroutine == null)
                return;
            
            StopCoroutine(_destroyCoroutine);
            
            _destroyCoroutine = null;
        }
    
        public void Move()
        {
            _soundEvents.PlaySound(ArriveSound);
            
            _pathTween?.Kill();
            _pathTween = _transform
                .DOPath(CalculatePath(), _duration)
                .SetEase(Ease.Linear)
                .OnUpdate(() => 
            {
                Vector3 direction = _transform.position - _previousPosition;
                
                if (direction.sqrMagnitude > 0) 
                {
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    
                    _rotationTween?.Kill();
                    _rotationTween = _transform
                        .DORotate(new Vector3(0, 0, angle), 0.1f)
                        .SetEase(Ease.OutSine);
                }
                
                _previousPosition = _transform.position;
            });
        }

        private Vector3[] CalculatePath()
        {
            Vector3 startPos = _transform.position;
            _previousPosition = startPos;
        
            var path = new Vector3[_pathResolution];
        
            for (int i = 0; i < _pathResolution; i++)
            {
                float t = i / (float)(_pathResolution - 1);
            
                float parabolicFunction = 4 * t * (1 - t);
            
                Vector3 point = Vector3.Lerp(startPos, _target, t);
            
                point.y += _arcHeight * parabolicFunction;
            
                path[i] = point;
            }
            
            return path;
        }

        private void StopMove()
        {
            _pathTween?.Kill();
            _rotationTween?.Kill();
        }
        
        private IEnumerator WaitForDestroy()
        {
            var wait = new WaitForSeconds(_timeToDestroy);
            yield return wait;

            ReturnToPool();
        }
    }
}