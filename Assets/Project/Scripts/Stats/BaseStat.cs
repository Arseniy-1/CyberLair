using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[Serializable]
public abstract class BaseStat
{
    [field: SerializeField] public float BaseValue { get; protected set; }
    private List<StatModifier> modifiers = new List<StatModifier>();

    [SerializeField] public float CurrentValue = 0;

    public void UpdateModifiers(float deltaTime)
    {
        for(int i = 0; i < modifiers.Count; i++)
        {
            modifiers[i].Update(deltaTime);
            
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

        foreach (var mod in modifiers.Where(mod => mod.Type == ModifierType.Multiplicative))
        {
            finalValue *= mod.Value;
        }

        return finalValue;
    }

    public void AddModifier(StatModifier modifier)
    {
        modifier.ValueExpired += RemoveModifier;
        modifiers.Add(modifier);
        CurrentValue = CalculateValue();
    }

    public void RemoveModifier(StatModifier modifier)
    {
        modifier.ValueExpired -= RemoveModifier;
        modifiers.Remove(modifier);
        CurrentValue = CalculateValue();
    }
}