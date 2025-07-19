using Project.Scripts.Interfaces;
using Project.Scripts.SkillSystem.SkillSOClasses;
using Project.Scripts.Stats;

namespace Project.Scripts.SkillSystem.SkillInstances
{
    public class FirstAid : ISkillInstance
    {
        private readonly Health _health;
        private readonly float _healProportion;
        
        public FirstAid(SkillData skillData, FirstAidSkill skill)
        {
            _health = skillData.PlayerStats.Health;
            _healProportion = skill.HealProportion;
            _health.DamageTaken += HealPart;
        }
        
        public void Disable()
        {
            _health.DamageTaken -= HealPart;
        }

        private void HealPart(float damage)
        {
            if (damage < 0)
                return;
            
            _health.Heal(damage * _healProportion);
        }
    }
}