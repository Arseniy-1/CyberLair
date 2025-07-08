using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Project.Scripts.PlayerSystem.TakeDamageEffect
{
    [Serializable]
    public class EntityDamageView
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Material _blinkMaterial;
        
        [SerializeField] private float _blinkDuration;
        
        private Material _defaultMaterial;
        private CancellationTokenSource _cancellationToken;
        
        public void Initialize()
        {
            _defaultMaterial = _spriteRenderer.material;
        }

        public virtual void StartBlink()
        {
            CancelBlink();
            
            Blink(_cancellationToken.Token).Forget();
        }
        
        public void EndBlink()
        {
            _spriteRenderer.material = _defaultMaterial;

            CancelBlink();
        }
        
        private void CancelBlink()
        {
            _cancellationToken?.Cancel();
            _cancellationToken = new CancellationTokenSource();
        }

        private async UniTaskVoid Blink(CancellationToken token)
        {
            _spriteRenderer.material = _blinkMaterial;

            await UniTask.Delay(TimeSpan.FromSeconds(_blinkDuration), cancellationToken: token);
            
            EndBlink();
        }
    }
}