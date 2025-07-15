using Project.Scripts.Interfaces;
using Project.Scripts.Skill;
using Project.Scripts.Stats;

namespace Project.Prefabs.Configs.Skills.Tireless
{
    public class Tireless : ISkillInstance
    {
        private readonly SkillData _data;
        private readonly StatModifier _jumpReloadTimeModifier;

        public Tireless(SkillData skillData, TirelessSkill skill)
        {
            _data = skillData;

            _jumpReloadTimeModifier = skill.JumpReloadTimeModifier.Copy();

            _data.PlayerStats.WeaponDamage.AddModifier(_jumpReloadTimeModifier);
        }

        public  void Disable()
        {
            _data.PlayerStats.WeaponDamage.RemoveModifier(_jumpReloadTimeModifier);
        }
    }
}