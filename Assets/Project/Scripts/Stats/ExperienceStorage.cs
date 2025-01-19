using System;

public class ExperienceStorage : Stats
{
    public event Action LevelRaised;

    public void AddExperience()
    {
        CurrentValue++;

        if (CurrentValue >= maxHealthValue)
            LevelRaised?.Invoke();

        RaiseAmountChanged();
    }

    public bool TrySpendExperience(int amount)
    {
        if(amount <= 0 )
            return false;

        if (CurrentValue - amount < 0)
            return false;

        CurrentValue -= amount;
        RaiseAmountChanged();

        return true;
    }
}
