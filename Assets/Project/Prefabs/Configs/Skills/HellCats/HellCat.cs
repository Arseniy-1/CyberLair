using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class HellCat : MonoBehaviour, IDestoyable<HellCat>
{
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

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _scanRadius, _targetLayer);
        
        var targets = new HashSet<ITarget>();

        foreach (Collider2D hit in hits)
            if (hit.TryGetComponent(out ITarget target))
                targets.Add(target);

        List<ITarget> sortedTargets = targets.OrderBy(target => (target.Position - position).magnitude).ToList();

        _target = sortedTargets.Count > 0 ? sortedTargets.ToArray()[0] : null;
        
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