using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Project.Scripts.EnemySystem
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Material _blinkMaterial;
        
        [SerializeField] private float _blinkDuration;
        
        [field: SerializeField] public Animator Animator { get; private set; }
        
        private Material _defaultMaterial;

        public void Initialize()
        {
            _defaultMaterial = _spriteRenderer.material;
        }

        public async UniTaskVoid Blink()
        {
            _spriteRenderer.material = _blinkMaterial;

            await UniTask.Delay(TimeSpan.FromSeconds(_blinkDuration));
            
            _spriteRenderer.material = _defaultMaterial;
        }
    }
}