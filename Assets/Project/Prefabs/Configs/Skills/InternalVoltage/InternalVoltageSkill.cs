using UnityEngine;

namespace Project.Prefabs.Configs.Skills.InternalVoltage
{
    public class InternalVoltageSkill : HardSkill
    {
        [SerializeField] private InternalVoltage _internalVoltage;
        
        public override void Apply(SkillData skillData)
        {
            _internalVoltage.Initialize(skillData.PlayerStats.Health, skillData.WeaponHolder.transform);
        }
    }
}