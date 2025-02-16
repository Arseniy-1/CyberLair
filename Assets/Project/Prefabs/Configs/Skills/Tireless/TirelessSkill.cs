using UnityEngine;

namespace Project.Prefabs.Configs.Skills
{
    [CreateAssetMenu(fileName = "PhantomArrowsSkill", menuName = "Skill/Hard/PhantomArrows", order = 51)]
    public class TirelessSkill : HardSkill
    {
        [SerializeField] private StatModifier _jumpReloadTimeModifier;
        
        public override void Apply(SkillData skillData)
        {
            skillData.PlayerStats.JumpReloadTime.AddModifier(_jumpReloadTimeModifier.Copy());
        }
    }
}