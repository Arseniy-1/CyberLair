using System;
using Project.Prefabs.Configs.Skills.Durability;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Prefabs.Configs.Skills.InternalVoltage
{
    public class InternalVoltage : SkillInstance
    {
        private readonly float _actionRadius;
        private readonly float _stunTime;
        private readonly LayerMask _layerMask;
        private readonly float _chance;
        
        private readonly Health _health;
        private readonly Transform _holder;

        private Vector2 Position => _holder.position;

        public InternalVoltage(SkillData skillData, InternalVoltageSkill skill)
        {
            _actionRadius = skill.ActionRadius;
            _stunTime = skill.StunTime;
            _layerMask = skill.LayerMask;
            _chance = skill.Chance;
            
            _holder = skillData.WeaponHolder.transform;
            _health = skillData.PlayerStats.Health;
            
            _health.DamageTaken += Shock;
        }

        private void Shock(float damage)
        {
            if(Random.value >= _chance)
                return;
            
            Collider2D[] colliders = Physics2D.OverlapCircleAll(Position, _actionRadius, _layerMask);

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

        public override void Disable()
        {
            _health.DamageTaken -= Shock;
        }
    }
}