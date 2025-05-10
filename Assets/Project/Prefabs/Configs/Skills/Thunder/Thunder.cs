using System;
using System.Collections.Generic;
using Project.Scripts.EnemySystem;
using UnityEngine;
using Object = UnityEngine.Object;
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
        private Transform _holder;
        private Weapon _weapon;
        
        private readonly List<CommonSkillView> _views = new();

        public Thunder(SkillData skillData, IThunderStats thunderSkill)
        {
            _actionRadius = thunderSkill.ActionRadius;
            _layerMask = thunderSkill.LayerMask;
            _damage = thunderSkill.Damage;
            _strikesCount = thunderSkill.StrikesCount;
            _shootsNeeded = thunderSkill.ShootsNeeded;

            _shootsPassed = 0;
            _holder = skillData.WeaponHolder.transform;
            _weapon = skillData.WeaponHolder.Weapon;

            for (int i = 0; i < _strikesCount; i++)
            {
                CommonSkillView commonSkillView = Object.Instantiate(thunderSkill.CommonSkillView);
                commonSkillView.EndPlaying();
                
                _views.Add(commonSkillView);
            }

            _weapon.Shot += HandleShoot;
        }

        public event Action<Enemy> EnemyStruck;

        public void Disable()
        {
            _weapon.Shot -= HandleShoot;
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
            Collider2D[] colliders = Physics2D.OverlapCircleAll(_holder.position, _actionRadius, _layerMask);

            for (int i = 0; i < _strikesCount; i++)
            {
                if (colliders.Length == 0)
                    return;

                Collider2D strickenCollider = colliders[Random.Range(0, colliders.Length)];

                if (strickenCollider.TryGetComponent(out Enemy affected) == false)
                    continue;
                
                affected.TakeDamage(_damage);
                
                CommonSkillView view = _views[i];
                view.transform.position = affected.transform.position;
                view.Initialize();
                
                EnemyStruck?.Invoke(affected);
            }
        }
    }
}