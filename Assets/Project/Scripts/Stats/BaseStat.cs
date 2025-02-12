using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[Serializable]
public abstract class BaseStat
{
    [field: SerializeField] public float BaseValue { get; protected set; }
    private List<StatModifier> modifiers = new();

    [SerializeField] public float CurrentValue = 0;

    public void CalculateCurrentValue()
    {
        CurrentValue = CalculateValue();
    }

    public void UpdateModifiers()
    {
        for(int i = 0; i < modifiers.Count; i++)
        {
            modifiers[i].Update();
            
            if (modifiers[i].HasExpired())
            {
                modifiers.RemoveAt(i);
            }
        }
    }

    protected virtual float CalculateValue()
    {
        float finalValue = BaseValue;
        
        finalValue = modifiers
            .Where(mod => mod.Type == ModifierType.Additive)
            .Aggregate(finalValue, (current, mod) => current + mod.Value);

        finalValue = modifiers.Where(mod => mod.Type == ModifierType.Multiplicative)
            .Aggregate(finalValue, (current, mod) => current * mod.Value);
        
        return finalValue;
    }

    public void AddModifier(StatModifier modifier)
    {
        modifier.ValueExpired += RemoveModifier;
        modifiers.Add(modifier);
        CalculateCurrentValue();
    }

    public void RemoveModifier(StatModifier modifier)
    {
        modifier.ValueExpired -= RemoveModifier;
        CalculateCurrentValue();
    }
}