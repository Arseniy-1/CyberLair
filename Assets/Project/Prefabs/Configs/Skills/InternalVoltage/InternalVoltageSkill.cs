using UnityEngine;

namespace Project.Prefabs.Configs.Skills.InternalVoltage
{
    [CreateAssetMenu(fileName = "InternalVoltageSkill", menuName = "Skill/Hard/InternalVoltage", order = 51)]
    public class InternalVoltageSkill : HardSkill
    {
        [SerializeField] private InternalVoltage _internalVoltage;
        
        public override void Apply(SkillData skillData)
        {
            _internalVoltage.Initialize(skillData.PlayerStats.Health, skillData.WeaponHolder.transform);
        }
    }
}