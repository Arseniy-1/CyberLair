using System;
using UnityEngine;

namespace Project.Scripts.Weapon.ActiveSkills.MagicArrow
{
    public class MagicArrow : ActiveWeapon, IDestoyable<MagicArrow>
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _speed;
        
        public event Action<MagicArrow> OnDestroyed;

        private void FixedUpdate()
        {
            _rigidbody.MovePosition(_rigidbody.position * (Time.fixedDeltaTime * _speed));
        }
    }
}