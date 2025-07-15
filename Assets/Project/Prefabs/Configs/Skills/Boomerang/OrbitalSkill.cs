using Project.Scripts.Services;
using Project.Scripts.Skill;
using UnityEngine;

namespace Project.Prefabs.Configs.Skills.Boomerang
{
    [CreateAssetMenu(fileName = "OrbitalSkill", menuName = "Skill/Simple/Orbital", order = 51)]
    public class OrbitalSkill : Skill
    {
        [field: SerializeField] public Orbital OrbitalPrefab { get; private set; }
    }
}