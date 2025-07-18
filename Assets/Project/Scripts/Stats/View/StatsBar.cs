using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.Stats.View
{
    public class StatsBar : StatsView
    {
        [SerializeField] protected Image StatsBarView;

        public override void ShowStats(float currentValue, float maxValue)
        {
            StatsBarView.fillAmount = currentValue / maxValue;
        }
    }
}