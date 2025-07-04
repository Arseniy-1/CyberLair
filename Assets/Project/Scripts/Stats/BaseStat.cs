using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[Serializable]
public abstract class BaseStat
{
    [field: SerializeField] public float BaseValue { get; protected set; }
    [field: SerializeField] public float CurrentValue { get; protected set; }
    
    private readonly List<StatModifier> _modifiers = new();

    public event Action<float, float> AmountChanged;
    
    public void CalculateCurrentValue()
    {
        CurrentValue = CalculateValue();
    }

    public virtual void Update()
    {
        foreach (var modifier in _modifiers.ToList()) 
        {
            modifier.Update();
        }
    }
    
    public void AddModifier(StatModifier modifier)
    {
        modifier.ValueExpired += RemoveModifier;
        _modifiers.Add(modifier);
        
        CalculateCurrentValue();
    }

    public void RemoveModifier(StatModifier modifier)
    {
        modifier.ValueExpired -= RemoveModifier;
        _modifiers.Remove(modifier);
        
        CalculateCurrentValue();
    }

    protected virtual float CalculateValue()
    {
        float finalValue = BaseValue;

        finalValue = _modifiers
            .Where(mod => mod.Type == ModifierType.Additive)
            .Aggregate(finalValue, (current, mod) => current + mod.Value);

        finalValue = _modifiers
            .Where(mod => mod.Type == ModifierType.Multiplicative)
            .Aggregate(finalValue, (current, mod) => current * mod.Value);

        return finalValue;
    }

    protected void OnAmountChanged(float currentValue, float baseValue)
    {
        AmountChanged?.Invoke(currentValue, baseValue);
    }
}