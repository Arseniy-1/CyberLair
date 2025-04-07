using System;
using System.Collections;
using Project.Scripts.Servises;
using UnityEngine;

namespace Project.Scripts.EnemySystem.Bosses.DeathReaper
{
    public class SoulOrbital : Orbital, IDestoyable<SoulOrbital>, IReturnable
    {
        [SerializeField] private float _timeToDestroy;
        
        private bool _isLocked;
        private Transform _playerTransform;

        private Coroutine _destroyCoroutine;

        public event Action<SoulOrbital> OnDestroyed;

        public override void Initialize(Transform targetTransform)
        {
            base.Initialize(targetTransform);
            
            _destroyCoroutine ??= StartCoroutine(WaitForDestroy());
        }

        public void ReturnToPool()
        {
            if (_destroyCoroutine == null)
                return;
            
            StopCoroutine(_destroyCoroutine);
            _destroyCoroutine = null;
        }

        private IEnumerator WaitForDestroy()
        {
            var wait = new WaitForSeconds(_timeToDestroy);
            yield return wait;
            
            OnDestroyed?.Invoke(this);
        }
    }
}