using UnityEngine;

namespace Project.Scripts.Servises
{
    public class Orbital : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _speed;
        [SerializeField] private float _radius;
        
        private float _angle;
        private Transform _targetTransform;
        
        private Vector3 CenterPosition => _targetTransform.position;
        
        protected virtual void FixedUpdate()
        {
            _angle += _speed * Time.fixedDeltaTime;

            Vector3 offset = new Vector2(Mathf.Cos(_angle) * _radius, Mathf.Sin(_angle) * _radius);
            Vector3 newPosition = CenterPosition + offset;

            _rigidbody.MovePosition(newPosition);
            
            Vector2 lookDirection = CenterPosition - (Vector3)_rigidbody.position;
            float lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
    
            _rigidbody.MoveRotation(lookAngle);
        }

        public virtual void Initialize(Transform targetTransform)
        {
            _targetTransform = targetTransform;
            CalculateOffset();
        }

        public void ApplyRadius(float radius)
        {
            if (radius < 0)
                return;
            
            _radius = radius;
        }
        
        private void CalculateOffset()
        {
            Vector3 offset = transform.position - CenterPosition;

            _angle = Mathf.Atan2(offset.y, offset.x);
        }
    }
}