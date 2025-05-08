using UnityEngine;
using UnityEngine.UI;
using System;

public class UnpauseButton : MonoBehaviour
{
    private Button _button;

    public event Action OnUnpause;
    
    private void OnEnable()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(Unpause);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(Unpause);
    }

    private void Unpause()
    {
        OnUnpause?.Invoke();
    }
}