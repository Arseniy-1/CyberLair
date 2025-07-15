using TMPro;
using UnityEngine;

namespace Project.Scripts.CompositionRoot
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _time;

        private float _timeElapsed;
        private int _minutes;
        private int _seconds;
        private float _nextUpdateTime;

        public string CurrentTime => _time.text;
        public int CurrentSeconds => _minutes * 60 + _seconds;
    
        private void FixedUpdate()
        {
            _timeElapsed += Time.fixedDeltaTime;

            if (_timeElapsed < _nextUpdateTime) 
                return;
        
            _nextUpdateTime = Mathf.Floor(_timeElapsed) + 1f;
            
            _seconds = Mathf.FloorToInt(_timeElapsed) % 60;
            _minutes = Mathf.FloorToInt(_timeElapsed) / 60;

            _time.text = $"{_minutes:00}:{_seconds:00}";
        }
    }
}