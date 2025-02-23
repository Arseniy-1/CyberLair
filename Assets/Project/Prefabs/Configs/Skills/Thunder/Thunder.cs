using System;
using Project.Prefabs.Configs.Skills.Durability;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Scripts.Weapon.ActiveSkills
{
    [Serializable]
    public class Thunder : ISkillInstance
    {
        private float _actionRadius;
        private LayerMask _layerMask;
        private int _damage;
        private float _strikesCount;
        private float _shootsNeeded;

        private float _shootsPassed;
        private Transform _target;
        private Weapon _weapon;

        private Vector2 Position => _target.position;

        public Thunder(SkillData skillData, IThunderStats thunderSkill)
        {
            _actionRadius = thunderSkill.ActionRadius;
            _layerMask = thunderSkill.LayerMask;
            _damage = thunderSkill.Damage;
            _strikesCount = thunderSkill.StrikesCount;
            _shootsNeeded = thunderSkill.ShootsNeeded;

            _shootsPassed = 0;
            _target = skillData.WeaponHolder.transform;
            _weapon = skillData.WeaponHolder.Weapon;

            _weapon.Shooted += HandleShoot;
        }

        public void Disable()
        {
            _weapon.Shooted -= HandleShoot;
        }

        private void HandleShoot(Bullet bullet)
        {
            _shootsPassed++;

            if (_shootsPassed < _shootsNeeded) return;

            _shootsPassed = 0;
            Strike();
        }

        private void Strike()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(Position, _actionRadius, _layerMask);

            for (int i = 0; i < _strikesCount; i++)
            {
                if (colliders.Length == 0)
                    return;

                Collider2D strickenCollider = colliders[Random.Range(0, colliders.Length)];

                if (strickenCollider.TryGetComponent(out IDamageable affected))
                {
                    affected.TakeDamage(_damage);
                }
            }
        }
    }
}