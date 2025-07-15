using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project.Scripts.Skill
{
    public abstract class HardSkill : global::Project.Scripts.Skill.Skill
    {
        [SerializeField] protected List<global::Project.Scripts.Skill.Skill> NeededSkills;

        public bool IsAvailable(List<global::Project.Scripts.Skill.Skill> skills)
        {
            return NeededSkills.All(skills.Contains);
        }
    }
}