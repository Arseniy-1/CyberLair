using System.Collections;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _time;

    private float _timeElapsed = 0f;
    private int _minutes = 0;
    private int _seconds = 0;

    private WaitForSeconds _secondWait;

    private void Start()
    {
        int secondAmount = 1;
        _secondWait = new WaitForSeconds(secondAmount);
        
        StartCoroutine(SettingTime());
    }

    private IEnumerator SettingTime()
    {
        while (true)
        {
            yield return _secondWait;

            _timeElapsed++;
            _seconds = Mathf.FloorToInt(_timeElapsed) % 60;
            _minutes = Mathf.FloorToInt(_timeElapsed) / 60;

            _time.text = string.Format("{0:00}:{1:00}", _minutes, _seconds);
        }
    }
}