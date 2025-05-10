using System;
using System.Collections;
using Project.Scripts.Services;
using Project.Scripts.Servises;
using UnityEngine;

namespace Project.Scripts.EnemySystem.Bosses.DeathReaper
{
    public class SoulOrbital : Orbital, IDestoyable<SoulOrbital>, IReturnable
    {
        private const string ArriveSound = "Arrive";
        
        [SerializeField] private float _timeToDestroy;
        [SerializeField] private SoundAnimationEvents _soundEvents;
        
        private bool _isLocked;
        private Transform _playerTransform;

        private Coroutine _destroyCoroutine;

        public event Action<SoulOrbital> OnDestroyed;

        public override void Initialize(Transform targetTransform)
        {
            base.Initialize(targetTransform);
            
            _soundEvents.PlaySound(ArriveSound);
            
            _destroyCoroutine ??= StartCoroutine(WaitForDestroy());
        }

        public void ReturnToPool()
        {
            if (_destroyCoroutine != null)
                StopCoroutine(_destroyCoroutine);
            
            _destroyCoroutine = null;
            
            OnDestroyed?.Invoke(this);
        }

        private IEnumerator WaitForDestroy()
        {
            var wait = new WaitForSeconds(_timeToDestroy);
            yield return wait;

            ReturnToPool();
        }
    }
}