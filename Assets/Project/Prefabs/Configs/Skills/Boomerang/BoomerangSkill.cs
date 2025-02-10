using Project.Scripts.Servises;
using UnityEngine;

[CreateAssetMenu(fileName = "BoomerangSkill", menuName = "Skill/Simple/Boomerang", order = 51)]
public class BoomerangSkill : Skill
{
    [SerializeField] private Orbital _boomerangPrefab;
    
    public override void Apply(SkillData skillData)
    {
        skillData.PlayerStats.OrbitalHandler.AddOrbital(_boomerangPrefab, skillData.WeaponHolder.transform);
    }
}