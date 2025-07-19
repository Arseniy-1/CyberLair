using Project.Scripts.Services;
using UnityEngine;

namespace Project.Scripts.SkillSystem.SkillSOClasses
{
    [CreateAssetMenu(fileName = "OrbitalSkill", menuName = "Skill/Simple/Orbital", order = 51)]
    public class OrbitalSkill : Skill
    {
        [field: SerializeField] public Orbital OrbitalPrefab { get; private set; }
    }
}