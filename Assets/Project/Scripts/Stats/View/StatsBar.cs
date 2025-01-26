using UnityEngine;
using UnityEngine.UI;

public class StatsBar : StatsView
{
    [SerializeField] protected Slider StatsBarView;

    protected override void ShowStats(int currentValue, int maxValue)
    {
        StatsBarView.value = (float)currentValue / maxValue;
    }
}
