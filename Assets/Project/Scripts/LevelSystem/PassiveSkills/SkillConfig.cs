using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class SkillConfig
{
    [SerializeField] private List<float> _multipliers;
    
    public IReadOnlyList<float> Multipliers => _multipliers;
}