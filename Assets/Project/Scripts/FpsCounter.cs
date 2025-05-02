using TMPro;
using UnityEngine;

public class FpsCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _fpsText;
    
    private float _deltaTime = 0.0f;

    private void Update()
    {
        _deltaTime += (Time.deltaTime - _deltaTime) * 0.1f;
    }

    private void LateUpdate()
    {
        float fps = 1.0f / _deltaTime;
        _fpsText.text = "FPS: " + fps;
    }
}