using Project.Scripts.Interfaces;
using Project.Scripts.SkillSystem.SkillSOClasses;
using Project.Scripts.Stats;

namespace Project.Scripts.SkillSystem.SkillInstances
{
    public class Harding : ISkillInstance
    {
        private readonly StatModifier _jumpReloadTimeModifier;
        private readonly SkillData _skillData;
        
        public Harding(SkillData skillData, HardingSkill hardingSkill)
        {
            _skillData = skillData;
            _jumpReloadTimeModifier = hardingSkill.JumpReloadTimeModifier.Copy();
            
            _skillData.PlayerStats.JumpReloadTime.AddModifier(_jumpReloadTimeModifier);
        }
        
        public void Disable()
        {
            _skillData.PlayerStats.JumpReloadTime.RemoveModifier(_jumpReloadTimeModifier);
        }
    }
}