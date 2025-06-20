using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Project.Scripts.EnemySystem
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Material _blinkMaterial;
        [SerializeField] private ParticleSystem _takeDamageParticles;
        
        [SerializeField] private float _blinkDuration;
        
        private Material _defaultMaterial;
        private CancellationTokenSource _cancellationToken;
        
        [field: SerializeField] public Animator Animator { get; private set; }
        
        public void Initialize()
        {
            _defaultMaterial = _spriteRenderer.material;
        }

        public void StartBlink()
        {
            _cancellationToken?.Cancel();
            _cancellationToken = new CancellationTokenSource();
            
            Blink(_cancellationToken.Token).Forget();
        }

        public void EndBlink()
        {
            _spriteRenderer.material = _defaultMaterial;
            
            _cancellationToken?.Cancel();
        }

        private async UniTaskVoid Blink(CancellationToken token)
        {
            _takeDamageParticles.Play();
            _spriteRenderer.material = _blinkMaterial;

            await UniTask.Delay(TimeSpan.FromSeconds(_blinkDuration), cancellationToken: token);
            
            EndBlink();
        }
    }
}