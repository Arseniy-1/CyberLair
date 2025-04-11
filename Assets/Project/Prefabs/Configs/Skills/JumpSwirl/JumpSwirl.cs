using System;
using Project.Scripts.EnemySystem;
using UnityEngine;

namespace Project.Prefabs.Configs.Skills.JumpSwirl
{
    public class JumpSwirl : ISkillInstance
    {
        private readonly SkillData _skillData;
        private readonly JumpSwirlSkill _skill;

        public JumpSwirl(SkillData skillData, JumpSwirlSkill skill)
        {
            _skillData = skillData;
            _skill = skill;

            _skillData.PlayerJumper.JumpPerformed += HandleJump;
        }

        public void Disable()
        {
            _skillData.PlayerJumper.JumpPerformed -= HandleJump;
        }

        private void HandleJump()
        {
            var playerPosition = _skillData.PlayerJumper.transform.position;
            var affectedColliders = Physics2D.OverlapCircleAll(playerPosition, _skill.KnockbackRadius, _skill.EnemyLayer);

            foreach (var collider in affectedColliders)
            {
                if (!collider.TryGetComponent(out Enemy enemy))
                    continue;
                
                if (Enum.IsDefined(typeof(BossTypes), enemy.EnemyType))
                    return;
                    
                enemy.TakeStun(_skill.StunTime);
                 
                Vector2 knockbackDirection = ((Vector3)enemy.Position - playerPosition).normalized;
                enemy.Rigidbody2D.AddForce(knockbackDirection * _skill.KnockbackForce, ForceMode2D.Impulse);
            }
        }
    }
}