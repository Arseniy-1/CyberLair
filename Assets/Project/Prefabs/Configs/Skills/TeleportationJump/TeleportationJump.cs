using Project.Scripts.Interfaces;
using Project.Scripts.Skill;
using Project.Scripts.Stats;

namespace Project.Prefabs.Configs.Skills.TeleportationJump
{
    public class TeleportationJump: ISkillInstance
    {
        private readonly StatModifier _jumpTimeModifier;
        private readonly StatModifier _jumpSpeedModifier;

        private readonly SkillData _skillData;

        public TeleportationJump(SkillData skillData, TeleportationJumpSkill skill)
        {
            _jumpTimeModifier = skill.JumpTimeModifier.Copy();
            _jumpSpeedModifier = skill.JumpSpeedModifier.Copy();
            _skillData = skillData;

            _skillData.PlayerStats.JumpTime.AddModifier(_jumpTimeModifier);
            _skillData.PlayerStats.JumpSpeed.AddModifier(_jumpSpeedModifier);
        }

        public void Disable()
        {
            _skillData.PlayerStats.JumpTime.RemoveModifier(_jumpTimeModifier);
            _skillData.PlayerStats.JumpSpeed.RemoveModifier(_jumpSpeedModifier);
        }
    }
}