using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.Settings
{
    public abstract class SettingToggle : MonoBehaviour
    {
        [SerializeField] protected Toggle Toggle;

        public virtual void Initialize()
        {
            Toggle.onValueChanged.AddListener(HandleToggle);
        }

        private void OnDisable()
        {
            Toggle.onValueChanged.RemoveListener(HandleToggle);
        }

        protected abstract void HandleToggle(bool isOn);
    }
}