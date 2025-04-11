using System;
using System.Linq;
using Project.Scripts.EnemySystem;
using Project.Scripts.Weapon.ActiveSkills;
using Random = UnityEngine.Random;

namespace Project.Prefabs.Configs.Skills.ThunderStorm
{
    public class ThunderStorm : ISkillInstance
    {
        private readonly float _chance;
        private readonly Thunder _thunder;

        public ThunderStorm(ThunderStormSkill skill, Thunder thunder)
        {
            _chance = skill.Chance;
            _thunder = thunder;

            _thunder.EnemyStruck += CriticalStrike;
        }
        
        public void Disable()
        {
            _thunder.EnemyStruck -= CriticalStrike;
        }

        private void CriticalStrike(Enemy enemy)
        {
            if (Random.value > _chance)
                return;

            if (Enum.IsDefined(typeof(BossTypes), enemy.EnemyType))
                return;

            enemy.Die();
        }
    }
}