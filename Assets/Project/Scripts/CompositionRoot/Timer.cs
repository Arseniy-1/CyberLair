using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _time;

    private float _timeElapsed = 0f;
    private int _minutes = 0;
    private int _seconds = 0;
    private float _nextUpdateTime = 0f;

    public string CurrentTime => _time.text.ToString();
    public int CurrentSeconds => _minutes * 60 + _seconds;
    
    private void FixedUpdate()
    {
        _timeElapsed += Time.fixedDeltaTime;
        
        if (_timeElapsed >= _nextUpdateTime)
        {
            _nextUpdateTime = Mathf.Floor(_timeElapsed) + 1f;
            
            _seconds = Mathf.FloorToInt(_timeElapsed) % 60;
            _minutes = Mathf.FloorToInt(_timeElapsed) / 60;

            _time.text = string.Format("{0:00}:{1:00}", _minutes, _seconds);
        }
    }
}