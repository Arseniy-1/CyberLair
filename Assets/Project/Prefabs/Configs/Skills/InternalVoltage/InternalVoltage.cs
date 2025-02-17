using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Prefabs.Configs.Skills.InternalVoltage
{
    [Serializable]
    public class InternalVoltage
    {
        [SerializeField] private float _actionRadius;
        [SerializeField] private float _stunTime;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField, Range(0f, 1f)] private float _chance;
        
        private Transform _target;

        private Vector2 TargetPosition => _target.position;

        public void Initialize(Health health, Transform target)
        {
            _target = target;
            health.DamageTaken += Shock;
        }

        private void Shock(float damage)
        {
            if(Random.value >= _chance)
                return;
            
            Collider2D[] colliders = Physics2D.OverlapCircleAll(TargetPosition, _actionRadius, _layerMask);

            if(colliders.Length == 0)
                    return;

            foreach (Collider2D strickenCollider in colliders)
            {
                if (strickenCollider.TryGetComponent(out IStunable affected))
                {
                    affected.TakeStun(_stunTime);
                }
            }
        }
    }
}