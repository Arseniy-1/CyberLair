using System;
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
        
        [field: SerializeField] public Animator Animator { get; private set; }
        
        public void Initialize()
        {
            _defaultMaterial = _spriteRenderer.material;
        }

        public async UniTaskVoid Blink()
        {
            _takeDamageParticles.Play();
            _spriteRenderer.material = _blinkMaterial;

            await UniTask.Delay(TimeSpan.FromSeconds(_blinkDuration));
            
            _spriteRenderer.material = _defaultMaterial;
        }
    }
}