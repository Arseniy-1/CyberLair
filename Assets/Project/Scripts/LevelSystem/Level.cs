using System;
using UnityEngine;

public class Level : MonoBehaviour
{
    private ExperienceStorage _experienceStorage;
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