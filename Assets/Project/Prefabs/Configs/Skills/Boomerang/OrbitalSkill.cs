using Project.Scripts.Servises;
using UnityEngine;

[CreateAssetMenu(fileName = "OrbitalSkill", menuName = "Skill/Simple/Orbital", order = 51)]
public class OrbitalSkill : Skill
{
    [SerializeField] private Orbital _orbitalPrefab;
    
    public Orbital OrbitalInstance { get; private set; }
    
    public override void Apply(SkillData skillData)
    {
        OrbitalInstance = Instantiate(_orbitalPrefab);
        skillData.PlayerStats.OrbitalHandler.AddOrbital(OrbitalInstance, skillData.WeaponHolder.transform);
    }
}