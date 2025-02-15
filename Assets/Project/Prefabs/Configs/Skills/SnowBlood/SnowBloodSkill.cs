using UnityEngine;

namespace Project.Prefabs.Configs.Skills.SnowBlood
{
    [CreateAssetMenu(fileName = "SnowBloodSkill", menuName = "Skill/Hard/SnowBlood", order = 51)]
    public class SnowBloodSkill : HardSkill
    {
        [SerializeField] private StatModifier _healthModifier;
        [SerializeField] private StatModifier _damageModifier;
        
        public override void Apply(SkillData skillData)
        {
            skillData.PlayerStats.Health.AddModifier(_healthModifier);
            skillData.PlayerStats.WeaponDamage.AddModifier(_damageModifier);
        }
    }
}