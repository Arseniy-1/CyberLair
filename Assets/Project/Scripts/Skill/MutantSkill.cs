using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class MutantSkill : Skill
{
    [SerializeField] private List<Skill> _neededSkills;

    public abstract override void Apply(SkillData skillData);

    public bool IsAvailable(List<Skill> skills)
    {
        return _neededSkills.All(skills.Contains);
    }
}