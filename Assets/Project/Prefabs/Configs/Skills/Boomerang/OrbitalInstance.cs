using Project.Scripts.Interfaces;
using Project.Scripts.Services;
using Project.Scripts.Skill;
using UnityEngine;

namespace Project.Prefabs.Configs.Skills.Boomerang
{
    public class OrbitalInstance : ISkillInstance
    {
        public Orbital Orbital { get; }

        protected OrbitalInstance(SkillData skillData, OrbitalSkill orbitalSkill)
        {
            Orbital = Object.Instantiate(orbitalSkill.OrbitalPrefab);
            skillData.PlayerStats.OrbitalHandler.AddOrbital(Orbital, skillData.WeaponHolder.transform);
        }

        public void Disable() { }
    }
}