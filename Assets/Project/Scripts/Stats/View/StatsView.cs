using UnityEngine;

public abstract class StatsView : MonoBehaviour
{
    private BaseStat _stats;

    private void OnDisable()
    {
        _stats.AmountChanged -= ShowStats;
    }

    public void Initialize(BaseStat stats)
    {
        _stats = stats;
        _stats.AmountChanged += ShowStats;
    }
    
    protected abstract void ShowStats(float currentValue, float maxValue);
}
