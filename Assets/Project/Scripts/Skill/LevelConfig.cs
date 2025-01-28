using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelConfig
{
    [SerializeField] private List<int> _experienceValues;
    
    public IReadOnlyList<int> ExperienceValues => _experienceValues;
}