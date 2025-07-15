using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Project.Scripts.Interfaces;
using UnityEngine;

namespace Project.Scripts.Spawners.ParticleEffects
{
    public class Effect : MonoBehaviour, IDestoyable<Effect>
    {
        [SerializeField] private List<ParticleSystem> _particles;

        private CancellationTokenSource _cancellationToken;
    
        public event Action<Effect> OnDestroyed;

        private void OnEnable()
        {
            _cancellationToken?.Cancel();
            _cancellationToken = new CancellationTokenSource();
        
            foreach (var particle in _particles)
            {
                particle.Play();
                WaitForParticleAsync(particle, _cancellationToken.Token).Forget();
            }
        }

        private void OnDisable()
        {
            _cancellationToken?.Cancel();
        }

        private async UniTaskVoid WaitForParticleAsync(ParticleSystem particle, CancellationToken token)
        {
            while (isActiveAndEnabled == false && particle.IsAlive(true))
            {
                await UniTask.Yield(cancellationToken: token);
            }

            OnDestroyed?.Invoke(this);
        }
    }
}