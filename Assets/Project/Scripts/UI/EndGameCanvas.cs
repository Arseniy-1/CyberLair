using TMPro;
using UnityEngine;

public class EndGameCanvas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timeView;

    public void ShowStats(string playTime)
    {
        _timeView.text = playTime;
    }
}
