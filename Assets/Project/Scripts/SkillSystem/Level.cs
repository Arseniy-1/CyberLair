using System;
using Project.Scripts.Stats;
using UnityEngine;

namespace Project.Scripts.SkillSystem
{
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

        private void UpgradeLevel()
        {
            _currentLevel++;
            LevelRaised?.Invoke();

            _experienceStorage.ResetExperience(_levelConfig.ExperienceValues[_currentLevel]);
        }
    }
}