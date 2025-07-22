using System;
using System.Collections.Generic;
using Project.Scripts.EnemySystem;
using Project.Scripts.Interfaces;
using Project.Scripts.SkillSystem.SkillViews;
using Project.Scripts.Weapon;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Project.Scripts.SkillSystem.SkillInstances
{
    public class Thunder : ISkillInstance
    {
        private const int MaxHits = 12;
        
        private readonly Collider2D[] _results = new Collider2D[MaxHits];
        
        private readonly float _actionRadius;
        private readonly LayerMask _layerMask;
        private readonly int _damage;
        private readonly float _strikesCount;
        private readonly float _shootsNeeded;

        private readonly Transform _holder;
        private readonly Weapon.Weapon _weapon;
        private readonly List<CommonSkillView> _views = new ();
        
        private float _shootsPassed;

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
            int hitCount = Physics2D.OverlapCircleNonAlloc(_holder.position, _actionRadius, _results, _layerMask);

            for (int i = 0; i < _strikesCount; i++)
            {
                if (hitCount == 0)
                    return;

                Collider2D strickenCollider = _results[Random.Range(0, hitCount)];

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