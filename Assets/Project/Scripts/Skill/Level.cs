using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private LevelConfig _levelConfig;

    private ExperienceStorage _experienceStorage;
    private int _currentLevel;

    public event Action LevelRaised;

    public void Initialize(ExperienceStorage experienceStorage)
    {
        _experienceStorage = experienceStorage;
        _experienceStorage.LevelRaised += UpgradeLevel;
        _experienceStorage.ResetExperience(_levelConfig.ExperienceValues[_currentLevel]);
    }

    private void OnDisable()
    {
        _experienceStorage.LevelRaised -= UpgradeLevel;
    }

    [Button]
    private void UpgradeLevel()
    {
        _currentLevel++;
        LevelRaised?.Invoke();

        _experienceStorage.ResetExperience(_levelConfig.ExperienceValues[_currentLevel]);
    }
}