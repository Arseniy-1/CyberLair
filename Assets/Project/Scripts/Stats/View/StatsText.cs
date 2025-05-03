using TMPro;
using UnityEngine;

public class StatsText : StatsView
{
    [SerializeField] protected TextMeshProUGUI CurrentValueText;
    [SerializeField] protected TextMeshProUGUI MaxValueText;

    protected override void ShowStats(float currentValue, float maxValue)
    {
        CurrentValueText.text = Mathf.Round(currentValue).ToString();
        MaxValueText.text = maxValue.ToString();
    }
}