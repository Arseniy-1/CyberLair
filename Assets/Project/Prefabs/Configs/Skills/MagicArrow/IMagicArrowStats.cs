using Project.Scripts.Weapon.ActiveSkills.MagicArrow;
using UnityEngine;

public interface IMagicArrowStats
{
    public float Radius { get; }
    public float Delay { get; }
    public LayerMask LayerMask { get; }
    public MagicArrow MagicArrowPrefab { get; }
}