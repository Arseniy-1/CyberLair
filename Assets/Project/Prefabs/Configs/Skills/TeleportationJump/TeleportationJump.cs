namespace Project.Prefabs.Configs.Skills.TeleportationJump
{
    public class TeleportationJump: ISkillInstance
    {
        private readonly StatModifier _jumpTimeModifier;
        private readonly StatModifier _jumpDistanceModifier;

        private readonly SkillData _skillData;

        public TeleportationJump(SkillData skillData, TeleportationJumpSkill skill)
        {
            _jumpTimeModifier = skill.JumpTimeModifier.Copy();
            _jumpDistanceModifier = skill.JumpDistanceModifier.Copy();
            _skillData = skillData;

            _skillData.PlayerStats.JumpTime.AddModifier(_jumpTimeModifier);
            _skillData.PlayerStats.JumpDistance.AddModifier(_jumpDistanceModifier);
        }

        public void Disable()
        {
            _skillData.PlayerStats.JumpTime.RemoveModifier(_jumpTimeModifier);
            _skillData.PlayerStats.JumpDistance.RemoveModifier(_jumpDistanceModifier);
        }
    }
}