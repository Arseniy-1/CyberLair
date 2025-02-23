using System;
using System.Linq;
using Project.Scripts.Weapon.ActiveSkills;
using UnityEngine;

[CreateAssetMenu(fileName = "OverloadSkill", menuName = "Skill/Hard/Overload", order = 51)]
public class OverloadSkill : HardSkill, IThunderStats
{
    [field: SerializeField] public float ActionRadius { get; private set; }
    [field: SerializeField] public LayerMask LayerMask { get; private set; }
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public float StrikesCount { get; private set; }
    [field: SerializeField] public float ShootsNeeded { get; private set; }
}