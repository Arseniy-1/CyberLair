using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneOpener : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private Button _button;

    private void OnEnable()
    {
        _button.onClick.AddListener(OpenScene);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OpenScene);
    }

    private void OpenScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("Scene name is empty! Укажи сцену в инспекторе.");
        }
    }
}