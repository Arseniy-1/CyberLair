using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Stats : MonoBehaviour
{
    [SerializeField] protected int MaxValue;
    [SerializeField] protected int CurrentValue;

    public int MaxAmount => MaxValue;
    
    public event Action<int, int> AmountChanged;

    protected void RaiseAmountChanged()
    {
        AmountChanged?.Invoke(CurrentValue, MaxValue);
    }
}

