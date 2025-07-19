using System;
using Project.Scripts.EnemySystem;
using Project.Scripts.Interfaces;
using Project.Scripts.Services.Enum;
using Project.Scripts.SkillSystem.SkillSOClasses;
using Project.Scripts.SkillSystem.SkillViews;
using Project.Scripts.Stats;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Project.Scripts.SkillSystem.SkillInstances
{
    public class InternalVoltage : ISkillInstance
    {
        private readonly float _actionRadius;
        private readonly float _stunTime;
        private readonly LayerMask _layerMask;
        private readonly float _chance;
        
        private readonly Health _health;
        private readonly Transform _holder;

        private readonly CommonSkillView _view;

        private Vector2 Position => _holder.position;

        public InternalVoltage(SkillData skillData, InternalVoltageSkill skill)
        {
            _actionRadius = skill.ActionRadius;
            _stunTime = skill.StunTime;
            _layerMask = skill.LayerMask;
            _chance = skill.Chance;

            _view = Object.Instantiate(skill.SkillView);
            _view.EndPlaying();
            
            _holder = skillData.WeaponHolder.transform;
            _health = skillData.PlayerStats.Health;
            
            _health.DamageTaken += Shock;
        }

        public void Disable()
        {
            _health.DamageTaken -= Shock;
        }
        
        private void Shock(float damage)
        {
            if(Random.value >= _chance)
                return;
            
            Collider2D[] colliders = Physics2D.OverlapCircleAll(Position, _actionRadius, _layerMask);
            _view.transform.position = Position;
            _view.Initialize();

            if(colliders.Length == 0)
                    return;

            foreach (Collider2D strickenCollider in colliders)
            {
                if (!strickenCollider.TryGetComponent(out Enemy enemy))
                    continue;
                
                if (Enum.IsDefined(typeof(BossTypes), (BossTypes)(int)enemy.EnemyType))
                    continue;
                    
                enemy.TakeStun(_stunTime);
            }
        }
    }
}