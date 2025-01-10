using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project.Scripts.Weapon.ActiveSkills
{
    public class Boomerang : ActiveWeapon
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _speed;
        
        private float _angle;

        private void FixedUpdate()
        {
            _angle += _speed * Time.fixedDeltaTime;

            Vector3 offset = new Vector2(Mathf.Cos(_angle) * ActionRadius, Mathf.Sin(_angle) * ActionRadius);
            Vector3 newPosition = TargetTransform.position + offset;

            _rigidbody.MovePosition(newPosition);
        }

        public void CalculateOffset()
        {
            Vector3 offset = transform.position - TargetTransform.position;

            _angle = Mathf.Atan2(offset.y, offset.x);
        }
    }
}