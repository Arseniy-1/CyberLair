public class BerserkHealthRegenerator 
{
    private const float CriticalHealthLevel = 0.3f;
    
    private StatModifier _healthRegeneratorModifier;
    private HealthRegenerateAmount _healthRegenerateAmount;
    
    public void Initialize(Health health, HealthRegenerateAmount healthRegenerateAmount)
    {
        _healthRegenerateAmount = healthRegenerateAmount;
        
        health.AmountChanged += OnHealthChanged;
    }
    
    private void OnHealthChanged(float maxHealth, float currentHealth)
    {
        if (currentHealth / maxHealth <= CriticalHealthLevel)
            _healthRegenerateAmount.AddModifier(_healthRegeneratorModifier);
        else
            _healthRegenerateAmount.RemoveModifier(_healthRegeneratorModifier);
    }
}