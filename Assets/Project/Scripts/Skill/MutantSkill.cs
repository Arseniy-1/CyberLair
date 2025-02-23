using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class MutantSkill : Skill
{
    [SerializeField] protected List<Skill> NeededSkills;

    public bool IsAvailable(List<Skill> skills)
    {
        return NeededSkills.All(skills.Contains);
    }
}