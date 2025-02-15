using System;
using System.Linq;
using Project.Prefabs.Configs.Skills.Boomerang;
using UnityEngine;

namespace Project.Prefabs.Configs.Skills.StormBlade
{
    [CreateAssetMenu(fileName = "StormBladeSkill", menuName = "Skill/Hard/StormBlade", order = 51)]
    public class StormBladeSkill : HardSkill
    {
        [SerializeField] private StormBlade _stormBlade;
        
        public override void Apply(SkillData skillData)
        {
            var boomerang = (BoomerangOrbital)skillData.PlayerStats.OrbitalHandler.Orbitals.FirstOrDefault(orbital =>
                    orbital.GetType() == typeof(BoomerangOrbital));
            
            if(boomerang == false)
                throw new InvalidCastException("Invalid BoomerangOrbital type");
            
            _stormBlade.Initialize(boomerang);
        }
    }
}