using UnityEngine;
using UnityEngine.UI;

public abstract class SettingToggle : MonoBehaviour
{
    [SerializeField] protected Toggle Toggle;

    protected virtual void OnEnable()
    {
        Toggle.onValueChanged.AddListener(HandleToggle);
    }

    private void OnDisable()
    {
        Toggle.onValueChanged.RemoveListener(HandleToggle);
    }

    protected abstract void HandleToggle(bool isOn);
}