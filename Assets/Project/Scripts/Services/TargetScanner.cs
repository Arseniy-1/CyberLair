using System.Collections;
using System.Collections.Generic;
using Project.Scripts.Interfaces;
using UnityEngine;

namespace Project.Scripts.Services
{
    public class TargetScanner : MonoBehaviour
    {
        [SerializeField] private float _scanRadius = 150f;
        [SerializeField] private LayerMask _targetLayer;
        [SerializeField] private float _scanDelay = 1f;
        [SerializeField] private int _maxColliders = 50;

        private WaitForSeconds _delay;

        private Collider2D[] _hitsBuffer;
        private readonly HashSet<ITarget> _targets = new ();
        private readonly List<ITarget> _sortedTargets = new ();

        public ITarget ClosestTarget { get; private set; }
        public bool HasTarget => ClosestTarget != null;

        private void Start()
        {
            _delay = new WaitForSeconds(_scanDelay);
            _hitsBuffer = new Collider2D[_maxColliders];
        
            StartCoroutine(Scanning());
        }

        private IEnumerator Scanning()
        {
            while (enabled)
            {
                yield return _delay;
            
                Scan();
            }
        }

        private void Scan()
        {
            int hitCount = Physics2D.OverlapCircleNonAlloc(transform.position, _scanRadius, _hitsBuffer, _targetLayer);
            _targets.Clear();
            Vector2 position = transform.position;

            for (int i = 0; i < hitCount; i++)
            {
                if (_hitsBuffer[i].TryGetComponent(out ITarget target))
                    _targets.Add(target);
            }

            _sortedTargets.Clear();
            _sortedTargets.AddRange(_targets);

            _sortedTargets.Sort((firstTarget, lastTarget) =>
                (firstTarget.Position - position).sqrMagnitude.CompareTo((lastTarget.Position - position).sqrMagnitude));

            ClosestTarget = _sortedTargets.Count > 0 ? _sortedTargets[0] : null;
        }
    }
}