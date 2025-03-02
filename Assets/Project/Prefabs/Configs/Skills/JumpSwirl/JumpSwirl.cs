using UnityEngine;

namespace Project.Prefabs.Configs.Skills.JumpSwirl
{
    public class JumpSwirl : ISkillInstance
    {
        private SkillData _skillData;
        private JumpSwirlSkill _skill;

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
            var enemies = Physics2D.OverlapCircleAll(playerPosition, _skill.KnockbackRadius, _skill.EnemyLayer);

            foreach (var enemy in enemies)
            {
                if (enemy.TryGetComponent(out IStunable stunable))
                {
                    stunable.TakeStun(_skill.StunTime);
                 
                    Vector2 knockbackDirection = (enemy.transform.position - playerPosition).normalized;
                    stunable.Rigidbody2D.AddForce(knockbackDirection * _skill.KnockbackForce, ForceMode2D.Impulse);
                }
            }
        }
    }
}