using Project.Scripts.Servises;
using Project.Scripts.Weapon.ActiveSkills;
using UnityEngine;

[CreateAssetMenu(fileName = "BoomerangSkill", menuName = "Skill/Simple/Boomerang", order = 51)]
public class BoomerangSkill : Skill
{
    [SerializeField] private Boomerang _boomerangPrefab;
    
    public override void Apply(SkillData skillData)
    {
        var boomerang = Instantiate(_boomerangPrefab);
        skillData.PlayerStats.OrbitalHandler.AddOrbital(boomerang, skillData.WeaponHolder.transform);
    }
}