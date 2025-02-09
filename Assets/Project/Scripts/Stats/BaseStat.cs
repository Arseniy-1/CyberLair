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

        float additive = modifiers
            .Where(mod => mod.Type == ModifierType.Additive)
            .Sum(mod => mod.Value);
        
        finalValue += additive;

        return modifiers.Where(mod => mod.Type == ModifierType.Multiplicative)
            .Aggregate(finalValue, (current, mod) => current * mod.Value);
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
        // modifiers.Remove(modifier);
        CalculateCurrentValue();
    }
}