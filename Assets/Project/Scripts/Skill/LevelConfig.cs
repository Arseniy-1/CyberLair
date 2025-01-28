using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelConfig
{
    [SerializeField] private List<float> _multipliers;
    
    public IReadOnlyList<float> Multipliers => _multipliers;

    private int MaxSize = 5;
    
    private void OnValidate()
    {
        if (_multipliers.Count > MaxSize)
        {
            _multipliers = _multipliers.GetRange(0, MaxSize);
        }
    }
}