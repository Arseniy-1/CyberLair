using UnityEngine;

namespace Project.Scripts.Weapon.ActiveSkills
{
    public class Boomerang : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _speed;
        [SerializeField] private float _radius;
        
        private float _angle;
        private Transform _targetTransform;
        
        private Vector3 CenterPosition => _targetTransform.position;

        public void Initialize(Transform targetTransform)
        {
            _targetTransform = targetTransform;
        }

        private void FixedUpdate()
        {
            _angle += _speed * Time.fixedDeltaTime;

            Vector3 offset = new Vector2(Mathf.Cos(_angle) * _radius, Mathf.Sin(_angle) * _radius);
            Vector3 newPosition = CenterPosition + offset;

            _rigidbody.MovePosition(newPosition);
        }

        public void CalculateOffset()
        {
            Vector3 offset = transform.position - CenterPosition;

            _angle = Mathf.Atan2(offset.y, offset.x);
        }

        public void ApplyStats(float speed)
        {
            _speed *= speed;
        }
    }
}