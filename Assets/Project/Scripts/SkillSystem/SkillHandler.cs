using System.Collections.Generic;
using System.Linq;

namespace Project.Scripts.SkillSystem
{
    public class SkillHandler
    {
        private readonly List<Skill> _availableSkills;
        private readonly List<Skill> _raisedSkills;
        private readonly List<MutantSkill> _mutantSkills;
        private readonly List<HardSkill> _hardSkills;

        public SkillHandler(List<Skill> simpleSkills, List<MutantSkill> mutantSkills, List<HardSkill> hardSkills)
        {
            _availableSkills = new List<Skill>(simpleSkills);
            _mutantSkills = mutantSkills;
            _hardSkills = hardSkills;
            _raisedSkills = new List<Skill>();
        }

        public IReadOnlyList<Skill> AvailableSkills => _availableSkills.AsReadOnly();

        public void ProcessSelectedSkills(List<Skill> selectedSkills)
        {
            foreach (var skill in selectedSkills)
            {
                _availableSkills.Remove(skill);
                _raisedSkills.Add(skill);

                RemoveFromSpecialLists(skill);
                UpdateAvailableSkills();
            }
        }

        private void RemoveFromSpecialLists(Skill skill)
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