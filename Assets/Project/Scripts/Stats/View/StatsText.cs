using TMPro;
using UnityEngine;

namespace Project.Scripts.Stats.View
{
    public class StatsText : StatsView
    {
        [SerializeField] protected TextMeshProUGUI CurrentValueText;
        [SerializeField] protected TextMeshProUGUI MaxValueText;

        public override void ShowStats(float currentValue, float maxValue)
        {
            CurrentValueText.text = currentValue.ToString("0");
            MaxValueText.text = maxValue.ToString("0");
        }
    }
}