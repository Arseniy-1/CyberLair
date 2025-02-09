using System;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public class StatModifier
{
    [field: SerializeField] public float Value { get; private set; }
    [field: SerializeField] public ModifierType Type { get; private set; }
    [field: SerializeField, MinValue(0)] public float Duration { get; private set; }

    private float elapsedTime;

    public event Action<StatModifier> ValueExpired;

    public void Update(float deltaTime)
    {
        if (Duration > 0f)
        {
            elapsedTime += deltaTime;
        }

        if (HasExpired())
        {
            ValueExpired?.Invoke(this);
        }
    }

    public bool HasExpired()
    {
        return Duration > 0f && elapsedTime >= Duration;
    }
}