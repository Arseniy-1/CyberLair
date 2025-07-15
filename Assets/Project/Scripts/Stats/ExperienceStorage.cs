using System;
using UnityEngine;

namespace Project.Scripts.Stats
{
    public class ExperienceStorage : BaseStat
    {
        public event Action LevelRaised;

        private float _maxValue;

        public void AddExperience(int amount)
        {
            if (amount <= 0)
                return;

            CurrentValue = Mathf.Clamp(CurrentValue + amount, 0, _maxValue);

            if (CurrentValue >= _maxValue)
                LevelRaised?.Invoke();

            OnAmountChanged(CurrentValue, _maxValue);
        }

        public void ResetExperience(int maxValue)
        {
            CurrentValue = 0;
            _maxValue = maxValue;

            OnAmountChanged(CurrentValue, _maxValue);
        }
    }
}