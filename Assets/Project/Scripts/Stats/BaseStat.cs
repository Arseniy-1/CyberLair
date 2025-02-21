using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[Serializable]
public abstract class BaseStat
{
    [field: SerializeField] public float BaseValue { get; protected set; }
    [field: SerializeField] public float CurrentValue { get; protected set; }
    
    private List<StatModifier> _modifiers = new();

    public event Action<float, float> AmountChanged;
    
    public void CalculateCurrentValue()
    {
        CurrentValue = CalculateValue();
        OnAmountChanged();
    }

    public virtual void Update()
    {
        for (int i = 0; i < _modifiers.Count; i++)
        {
            _modifiers[i].Update();
        }
    }

    protected virtual float CalculateValue()
    {
        float finalValue = BaseValue;

        finalValue = _modifiers
            .Where(mod => mod.Type == ModifierType.Additive)
            .Aggregate(finalValue, (current, mod) => current + mod.Value);

        finalValue = _modifiers.Where(mod => mod.Type == ModifierType.Multiplicative)
            .Aggregate(finalValue, (current, mod) => current * mod.Value);

        return finalValue;
    }

    protected void OnAmountChanged()
    {
        AmountChanged?.Invoke(CurrentValue, BaseValue);
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
}