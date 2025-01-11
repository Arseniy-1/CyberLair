using System.Collections.Generic;
using UnityEngine;

public class SkillHolder
{
    private Dictionary<ISkill, int> _skills = new Dictionary<ISkill, int>();
    private int _maxSkillsCount = 5;

    public IReadOnlyDictionary<ISkill, int> Skills => _skills;

    public void AddSkill(ISkill skill)
    {
        int startSkillLevel = 1;

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