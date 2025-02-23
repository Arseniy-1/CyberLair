using System;
using System.Linq;
using Project.Scripts.LevelSystem.ActiveSkills;
using Project.Scripts.Weapon.ActiveSkills.MagicArrow;
using UnityEngine;

[CreateAssetMenu(fileName = "PhantomArrowsSkill", menuName = "Skill/Hard/PhantomArrows", order = 51)]
public class PhantomArrowsSkill : HardSkill
{
    [field: SerializeField] public MagicArrow PhantomArrowPrefab { get; private set; }
}