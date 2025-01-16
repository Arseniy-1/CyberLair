using UnityEngine;

namespace Project.Scripts.Weapon.ActiveSkills
{
    public class Boomerang : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _speed;
        [SerializeField] private float _radius;
        
        private float _angle;
        private Vector3 _centerPosition;

        public void Initialize(Vector3 centerPosition)
        {
            _centerPosition = centerPosition;
        }

        private void FixedUpdate()
        {
            _angle += _speed * Time.fixedDeltaTime;

            Vector3 offset = new Vector2(Mathf.Cos(_angle) * _radius, Mathf.Sin(_angle) * _radius);
            Vector3 newPosition = _centerPosition + offset;

            _rigidbody.MovePosition(newPosition);
        }

        public void CalculateOffset()
        {
            Vector3 offset = transform.position - _centerPosition;

            _angle = Mathf.Atan2(offset.y, offset.x);
        }

        public void ApplyStats(float speed)
        {
            _speed *= speed;
        }
    }
}