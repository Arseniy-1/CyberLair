using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private ExperienceStorage _experienceStorage;
    [SerializeField] private LevelConfig _levelConfig;
    
    private int _currentLevel = 0;

    public event Action LevelRaised;

    private void OnEnable()
    {
        _experienceStorage.LevelRaised += UpgradeLevel;
        _experienceStorage.ResetExperience(_levelConfig.ExperienceValues[_currentLevel]);
    }
    
    private void UpgradeLevel()
    {
        _currentLevel++;
        LevelRaised?.Invoke();
        
        _experienceStorage.ResetExperience(_levelConfig.ExperienceValues[_currentLevel]);
    }
}