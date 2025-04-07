using System;
using DG.Tweening;
using UnityEngine;

namespace Project.Scripts.EnemySystem.Bosses.DeathReaper
{
    public class SoulClot : MonoBehaviour, IDestoyable<SoulClot>, IReturnable
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private float _duration = 1f;
        [SerializeField] private float _arcHeight = 2f;
        [SerializeField] private int _pathResolution = 10;
        
        private Vector3 _target;
        private Vector3 _previousPosition;
        private Tweener _moveTween;
        
        public event Action<SoulClot> OnDestroyed;

        public void Initialize(Vector3 target)
        {
            _target = target;
        }
        
        public void ReturnToPool()
        {
            StopMove();
            
            OnDestroyed?.Invoke(this);
        }
    
        public void Move()
        {
            _transform.DOPath(CalculatePath(), _duration).SetEase(Ease.Linear).OnUpdate(() => 
            {
                Vector3 direction = _transform.position - _previousPosition;
                
                if (direction.sqrMagnitude > 0.0001f) 
                {
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    
                    _transform.DORotate(new Vector3(0, 0, angle), 0.1f).SetEase(Ease.OutSine);
                }
                
                _previousPosition = _transform.position;
            });
        }

        private Vector3[] CalculatePath()
        {
            Vector3 startPos = _transform.position;
            _previousPosition = startPos;
        
            Vector3[] path = new Vector3[_pathResolution];
        
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
            if (_moveTween != null && _moveTween.IsActive())
            {
                _moveTween.Kill();
                _moveTween = null;
            }
        }
    }
}