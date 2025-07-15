using Project.Scripts.Stats;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.UI
{
    public class InjuredScreenView : MonoBehaviour
    {
        [SerializeField] private Image targetImage;

        [SerializeField] private float _minPulseSpeed = 5f;
        [SerializeField] private float _maxPulseSpeed = 15f;
        [SerializeField] private float _pulseAmplitude = 0.1f;

        private Health _health; 
    
        private float _baseAlpha;
        private float _pulseSpeed = 1f;

        private void OnDestroy()
        {
            _health.AmountChanged -= OnHealthChanged;
        }

        public void Initialize(Health health)
        {
            _health = health;
            _health.AmountChanged += OnHealthChanged;
        }
    
        private void OnHealthChanged(float currentValue, float baseValue)
        {
            float healthPercent = Mathf.Clamp01(currentValue / baseValue);

            _baseAlpha = 1f - healthPercent;

            _pulseSpeed = Mathf.Lerp(_maxPulseSpeed, _minPulseSpeed, healthPercent);
        }

        private void Update()
        {
            if (_baseAlpha <= 0f)
            {
                SetImageAlpha(0f);

                return;
            }

            float pulse = Mathf.Sin(Time.time * _pulseSpeed) * _pulseAmplitude;
            float finalAlpha = Mathf.Clamp01(_baseAlpha + pulse);

            SetImageAlpha(finalAlpha);
        }

        private void SetImageAlpha(float alpha)
        {
            Color color = targetImage.color;
            color.a = alpha;
            targetImage.color = color;
        }
    }
}