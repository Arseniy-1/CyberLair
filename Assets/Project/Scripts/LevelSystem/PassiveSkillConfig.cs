using UnityEngine;
using System.Collections.Generic;

public class PassiveSkillConfig
{
    [SerializeField] private List<float> _multipliers;
    
    public IReadOnlyList<float> Multipliers => _multipliers;
}