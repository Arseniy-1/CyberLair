using UnityEngine;

public abstract class StatsView : MonoBehaviour
{
    [SerializeField] private Stats _stats;

    private void OnEnable()
    {
        _stats.AmountChanged += ShowStats;
    }

    private void OnDisable()
    {
        _stats.AmountChanged -= ShowStats;
    }

    protected abstract void ShowStats(int currentValue, int maxValue);
}
