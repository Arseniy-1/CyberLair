using UnityEngine;
using UnityEngine.UI;

public abstract class SettingSlider : MonoBehaviour
{
    [SerializeField] protected Slider Slider; 
    
    public virtual void Initialize()
    {
        Slider.onValueChanged.AddListener(HandleSliderValueChanged);  
    }

    private void OnDisable()
    {
        Slider.onValueChanged.RemoveListener(HandleSliderValueChanged);
    }

    protected abstract void HandleSliderValueChanged(float amount);
}