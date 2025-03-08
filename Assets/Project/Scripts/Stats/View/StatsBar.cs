using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class StatsBar : StatsView
{
    [SerializeField] protected Image StatsBarView;

    protected override void ShowStats(float currentValue, float maxValue)
    {
        StatsBarView.fillAmount = currentValue / maxValue;
    }
}