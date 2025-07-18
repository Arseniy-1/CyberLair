using UnityEngine;

namespace Project.Scripts.Stats.View
{
    public abstract class StatsView : MonoBehaviour
    {
        private BaseStat _stats;

        private void OnDestroy()
        {
            if (_stats != null)
                _stats.AmountChanged -= ShowStats;
        }
    
        public void Initialize(BaseStat stats)
        {
            _stats = stats;
            _stats.AmountChanged += ShowStats;
        }
    
        public abstract void ShowStats(float currentValue, float maxValue);
    }
}
