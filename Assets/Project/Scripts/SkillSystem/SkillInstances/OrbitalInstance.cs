using Project.Scripts.Interfaces;
using Project.Scripts.Services;
using Project.Scripts.SkillSystem.SkillSOClasses;
using UnityEngine;

namespace Project.Scripts.SkillSystem.SkillInstances
{
    public class OrbitalInstance : ISkillInstance
    {
        protected OrbitalInstance(SkillData skillData, OrbitalSkill orbitalSkill)
        {
            Orbital = Object.Instantiate(orbitalSkill.OrbitalPrefab);
            skillData.PlayerStats.OrbitalHandler.AddOrbital(Orbital, skillData.WeaponHolder.transform);
        }
        
        public Orbital Orbital { get; }

        public void Disable() { }
    }
}