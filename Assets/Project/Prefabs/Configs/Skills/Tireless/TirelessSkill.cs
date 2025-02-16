using UnityEngine;

namespace Project.Prefabs.Configs.Skills
{
    [CreateAssetMenu(fileName = "TirelessSkill", menuName = "Skill/Hard/Tireless", order = 51)]
    public class TirelessSkill : HardSkill
    {
        [SerializeField] private StatModifier _jumpReloadTimeModifier;
        
        public override void Apply(SkillData skillData)
        {
            skillData.PlayerStats.JumpReloadTime.AddModifier(_jumpReloadTimeModifier.Copy());
        }
    }
}