using System;
using System.Collections;
using System.Linq;
using Project.Scripts.Interfaces;
using Project.Scripts.Services.Enum;
using Project.Scripts.Services.Extensions;
using UnityEngine;

namespace Project.Scripts.SkillSystem.SkillViews
{
    public class HellCat : MonoBehaviour, IDestoyable<HellCat>
    {
        private const int MaxHits = 12;
        
        private readonly Collider2D[] _results = new Collider2D[MaxHits];
        
        [SerializeField] private LayerMask _targetLayer;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private AudioID _audio = AudioID.HellCat;
    
        [SerializeField, Header("Hell Cat Stats")] private float _speed;
        [SerializeField] private float _damage;
        [SerializeField] private float _scanRadius = 150;
        [SerializeField] private float _timeToDespawn = 6f;
    
        private ITarget _target;
        private Coroutine _timer;
    
        public event Action<HellCat> OnDestroyed;

        private void OnEnable()
        {
            _audio.Play();
        
            EndTimer();
        
            _timer = StartCoroutine(DespawnTimer());
        
            FindTarget();
        }

        private void FixedUpdate()
        {
            if (_target == null)
                return;
        
            var newPosition = Vector2
                .MoveTowards(_rigidbody.position, _target.Position, _speed * Time.fixedDeltaTime);
        
            _rigidbody.MovePosition(newPosition);
        }
    
        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out IDamageable damageable))
                damageable.TakeDamage(_damage);
        
            OnDestroyed?.Invoke(this);
        }

        private void OnDisable()
        {
            EndTimer();
        }
    
        private void FindTarget()
        {
            Vector2 position = transform.position;
            
            int hitCount = Physics2D.OverlapCircleNonAlloc(position, _scanRadius, _results, _targetLayer);
            
            _target = _results
                .Take(hitCount)
                .Select(hit =>
                {
                    hit.TryGetComponent(out ITarget target);
                    
                    return target;
                })
                .Where(target => target != null)
                .OrderBy(target => Vector2.Distance(position, target.Position))
                .FirstOrDefault();
        
            if (_target != null) 
                _target.OnDeath += OnTargetDeath;
        }

        private void OnTargetDeath()
        {
            _target.OnDeath -= OnTargetDeath;

            FindTarget();
        }

        private void EndTimer()
        {
            if(_timer != null)
                StopCoroutine(_timer);
        
            _timer = null;
        }

        private IEnumerator DespawnTimer()
        {
            var wait = new WaitForSeconds(_timeToDespawn);
        
            yield return wait;
        
            OnDestroyed?.Invoke(this);
        }
    }
}