using UnityEngine;

public abstract class StatsView : MonoBehaviour
{
    [SerializeField] private BaseStat _stats;

    private void OnEnable()
    {
        _stats.AmountChanged += ShowStats;
    }

    private void OnDisable()
    {
        _stats.AmountChanged -= ShowStats;
    }

    protected abstract void ShowStats(float currentValue, float maxValue);
}
