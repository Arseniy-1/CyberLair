using Project.Scripts.Interfaces;
using Project.Scripts.Skill;
using Project.Scripts.Stats;

namespace Project.Prefabs.Configs.Skills.ReactiveBoots
{
    public class ReactiveBoots : ISkillInstance
    {
        private readonly SkillData _data;
        private readonly StatModifier _speedModifier;
    
        public ReactiveBoots(SkillData data, ReactiveBootsSkill skill)
        {
            _data = data;
            _speedModifier = skill.SpeedModifier.Copy();
        
            _data.PlayerStats.Speed.AddModifier(_speedModifier);
        } 
    
        public void Disable()
        {
            _data.PlayerStats.Speed.RemoveModifier(_speedModifier);
        }
    }
}