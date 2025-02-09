using System;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public class StatModifier
{
    [field: SerializeField] public float Value { get; private set; }
    [field: SerializeField] public ModifierType Type { get; private set; }
    [field: SerializeField, MinValue(0)] public float Duration { get; private set; }

    [SerializeField] private float elapsedTime = 0;

    public event Action<StatModifier> ValueExpired;

    public void Update(float deltaTime)
    {
        if (Duration > 0f)
        {
            elapsedTime += deltaTime;
        }

        if (HasExpired())
        {
            Debug.Log("Expired");
            ValueExpired?.Invoke(this);
        }
    }

    public bool HasExpired()
    {
        Debug.Log(elapsedTime);
        Debug.Log(Duration);
        return elapsedTime >= Duration;
    }
}