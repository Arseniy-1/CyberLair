using UnityEngine;
using UnityEngine.UI;

public class StatsBar : StatsView
{
    [SerializeField] protected Slider StatsBarView;

    protected override void ShowStats(float currentValue, float maxValue)
    {
        StatsBarView.value = currentValue / maxValue;
    }
}
