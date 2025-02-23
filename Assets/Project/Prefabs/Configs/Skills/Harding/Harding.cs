using Project.Prefabs.Configs.Skills.Durability;

namespace Project.Prefabs.Configs.Skills.Harding
{
    public class Harding : SkillInstance
    {
        private readonly StatModifier _jumpReloadTimeModifier;
        private readonly SkillData _skillData;
        
        public Harding(SkillData skillData, HardingSkill hardingSkill)
        {
            _skillData = skillData;
            _jumpReloadTimeModifier = hardingSkill.JumpReloadTimeModifier.Copy();
            
            _skillData.PlayerStats.JumpReloadTime.AddModifier(_jumpReloadTimeModifier);
        }
        
        public override void Disable()
        {
            _skillData.PlayerStats.JumpReloadTime.RemoveModifier(_jumpReloadTimeModifier);
        }
    }
}