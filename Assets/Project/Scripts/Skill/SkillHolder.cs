using System.Collections.Generic;
using UnityEngine;

public class SkillHolder
{
    private Dictionary<Skill, int> _skills = new Dictionary<Skill, int>();
    private int _maxSkillsCount = 5;

    public IReadOnlyDictionary<Skill, int> Skills => _skills;

    public void AddSkill(Skill skill)
    {
        int startSkillLevel = 0;

        if (_skills.Count != 0)
        {
            if (_skills.ContainsKey(skill))
            {
                _skills[skill]++;

                return;
            }

            if (_skills.Count >= _maxSkillsCount)
            {
                return;
            }
        }

        _skills.Add(skill, startSkillLevel);
    }
}