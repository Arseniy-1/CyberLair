using Project.Scripts.Servises;
using UnityEngine;

[CreateAssetMenu(fileName = "OrbitalSkill", menuName = "Skill/Simple/Orbital", order = 51)]
public class OrbitalSkill : Skill
{
    [field: SerializeField] public Orbital OrbitalPrefab { get; private set; }
}