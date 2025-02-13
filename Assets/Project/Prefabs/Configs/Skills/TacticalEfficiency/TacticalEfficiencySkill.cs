using UnityEngine;

namespace Project.Prefabs.Configs.Skills.TacticalEfficiency
{
    [CreateAssetMenu(fileName = "TacticalEfficiencySkill", menuName = "Skill/Simple/TacticalEfficiency", order = 51)]
    public class TacticalEfficiencySkill : Skill
    {
        [SerializeField] private StatModifier _healthModifier;
        [SerializeField] private StatModifier _damageModifier;
        
        public override void Apply(SkillData skillData)
        {
            skillData.PlayerStats.Health.AddModifier(_healthModifier.Copy());
            skillData.PlayerStats.WeaponDamage.AddModifier(_damageModifier.Copy());
            
        }
    }
}