using System;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private ExperienceStorage _experienceStorage;
    [SerializeField] private LevelConfig _levelConfig;
    
    private int _currentLevel;

    public event Action LevelRaised;

    private void OnEnable()
    {
        _experienceStorage.LevelRaised += UpgradeLevel;
    }

    private void UpgradeLevel()
    {
        _currentLevel++;
        LevelRaised?.Invoke();
    }
}