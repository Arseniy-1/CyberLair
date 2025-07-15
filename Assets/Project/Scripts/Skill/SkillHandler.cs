using System.Collections.Generic;
using System.Linq;

namespace Project.Scripts.Skill
{
    public class SkillHandler
    {
        private readonly List<global::Project.Scripts.Skill.Skill> _availableSkills;
        private readonly List<global::Project.Scripts.Skill.Skill> _raisedSkills;
        private readonly List<MutantSkill> _mutantSkills;
        private readonly List<HardSkill> _hardSkills;

        public SkillHandler(List<global::Project.Scripts.Skill.Skill> simpleSkills, List<MutantSkill> mutantSkills, List<HardSkill> hardSkills)
        {
            _availableSkills = new List<global::Project.Scripts.Skill.Skill>(simpleSkills);
            _mutantSkills = mutantSkills;
            _hardSkills = hardSkills;
            _raisedSkills = new List<global::Project.Scripts.Skill.Skill>();
        }

        public IReadOnlyList<global::Project.Scripts.Skill.Skill> AvailableSkills => _availableSkills.AsReadOnly();

        public void ProcessSelectedSkills(List<global::Project.Scripts.Skill.Skill> selectedSkills)
        {
            foreach (var skill in selectedSkills)
            {
                _availableSkills.Remove(skill);
                _raisedSkills.Add(skill);

                RemoveFromSpecialLists(skill);
                UpdateAvailableSkills();
            }
        }

        private void RemoveFromSpecialLists(global::Project.Scripts.Skill.Skill skill)
        {
            if (_hardSkills.Contains(skill))
                _hardSkills.Remove((HardSkill)skill);

            if (_mutantSkills.Contains(skill))
                _mutantSkills.Remove((MutantSkill)skill);
        }

        private void UpdateAvailableSkills()
        {
            IEnumerable<HardSkill> hardSkills = _hardSkills
                .Concat(_mutantSkills)
                .Where(hardSkill => hardSkill.IsAvailable(_raisedSkills));
    
            foreach (var skill in hardSkills)
            {
                if (_availableSkills.Contains(skill) == false)
                    _availableSkills.Add(skill);
            }
        }
    }
}