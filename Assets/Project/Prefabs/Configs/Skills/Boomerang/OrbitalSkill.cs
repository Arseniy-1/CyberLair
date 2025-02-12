using Project.Scripts.Servises;
using UnityEngine;

[CreateAssetMenu(fileName = "OrbitalSkill", menuName = "Skill/Simple/Orbital", order = 51)]
public class OrbitalSkill : Skill
{
    [SerializeField] private Orbital _orbitalPrefab;
    
    public override void Apply(SkillData skillData)
    {
        var orbital = Instantiate(_orbitalPrefab);
        skillData.PlayerStats.OrbitalHandler.AddOrbital(orbital, skillData.WeaponHolder.transform);
    }
}