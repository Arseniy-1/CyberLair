using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project.Scripts.SkillSystem
{
    public abstract class HardSkill : Skill
    {
        [SerializeField] protected List<Skill> NeededSkills;

        public bool IsAvailable(List<Skill> skills)
        {
            return NeededSkills.All(skills.Contains);
        }
    }
}