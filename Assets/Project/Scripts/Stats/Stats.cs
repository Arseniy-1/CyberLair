using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Stats : MonoBehaviour
{
    [SerializeField] protected int maxHealthValue;
    [SerializeField] protected int CurrentValue;

    public int MaxValue => maxHealthValue;
    
    public event Action<int, int> AmountChanged;

    protected void RaiseAmountChanged()
    {
        AmountChanged?.Invoke(CurrentValue, maxHealthValue);
    }
}

