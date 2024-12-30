using System;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField] protected int MaxValue;
    [SerializeField] protected int CurrentValue;

    public event Action<int, int> AmountChanged;

    protected void RaiseAmountChanged()
    {
        AmountChanged?.Invoke(CurrentValue, MaxValue);
    }
}

