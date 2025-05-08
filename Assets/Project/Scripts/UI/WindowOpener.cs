using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class WindowOpener : MonoBehaviour
{
    [SerializeField] private Window _nextWindow;
    [SerializeField] private Window _currentWindow;

    private Button _button;

    private void OnEnable()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(ToggleWindow);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(ToggleWindow);
    }

    private void ToggleWindow()
    {
        _nextWindow.gameObject.SetActive(true);
        _currentWindow.gameObject.SetActive(false);
    }
}