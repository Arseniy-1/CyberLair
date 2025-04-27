using Project.Scripts.EnemySystem;
using UnityEngine;
using UnityEngine.UI;

public class BossStatsBar : StatsView
{
    [SerializeField] protected Image StatsBarView;
    [SerializeField] protected Enemy Boss;

    private void Start()
    {
        Initialize(Boss.EnemyStats.Health);
    }
    
    protected override void ShowStats(float currentValue, float maxValue)
    {
        StatsBarView.fillAmount = currentValue / maxValue;
    }
}