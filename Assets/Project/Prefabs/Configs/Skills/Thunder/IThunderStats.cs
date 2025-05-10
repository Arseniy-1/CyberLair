using Project.Scripts.Weapon.ActiveSkills;
using UnityEngine;

public interface IThunderStats
{
    float ActionRadius { get; }
    LayerMask LayerMask { get; }
    int Damage { get; }
    float StrikesCount { get; }
    float ShootsNeeded { get; }
    CommonSkillView CommonSkillView { get; }
}