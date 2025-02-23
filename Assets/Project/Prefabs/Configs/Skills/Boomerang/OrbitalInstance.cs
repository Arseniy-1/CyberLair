using Project.Prefabs.Configs.Skills.Durability;
using Project.Scripts.Servises;
using UnityEngine;

namespace Project.Prefabs.Configs.Skills.Boomerang
{
    public class OrbitalInstance : SkillInstance
    {
        public Orbital Orbital { get; private set; }
    
        public OrbitalInstance(SkillData skillData, OrbitalSkill orbitalSkill)
        {
            Orbital = Object.Instantiate(orbitalSkill.OrbitalPrefab);
            skillData.PlayerStats.OrbitalHandler.AddOrbital(Orbital, skillData.WeaponHolder.transform);
        }

        public override void Disable()
        {
            Object.Destroy(Orbital.gameObject);
        }
    }
    
    public class Boomerang : OrbitalInstance
    {
        public Boomerang(SkillData skillData, OrbitalSkill orbitalSkill) : base(skillData, orbitalSkill) { }
    }
}