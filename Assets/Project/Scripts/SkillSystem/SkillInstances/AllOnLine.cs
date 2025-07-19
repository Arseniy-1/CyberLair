using Project.Scripts.Interfaces;
using Project.Scripts.SkillSystem.SkillSOClasses;
using Project.Scripts.Stats;

namespace Project.Scripts.SkillSystem.SkillInstances
{
    public class AllOnLine : ISkillInstance
    {
        private readonly StatModifier _damageModifier;
        private readonly SkillData _skillData;

        public AllOnLine(SkillData skillData, AllOnLineSkill skill)
        {
            _damageModifier = skill.DamageModifier.Copy();
            _skillData = skillData;

            float damage = skillData.PlayerStats.Health.CurrentValue +
                skillData.PlayerStats.Health.ShieldAmount.CurrentValue - 1;
            
            skillData.PlayerStats.Health.TakeDamage(damage);
            skillData.PlayerStats.WeaponDamage.AddModifier(_damageModifier);
        }
        
        public void Disable()
        {
            _skillData.PlayerStats.WeaponDamage.RemoveModifier(_damageModifier);
        }
    }
}