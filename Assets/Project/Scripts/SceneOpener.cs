using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SceneOpener : MonoBehaviour
{
    [SerializeField] private string _sceneName;
    [SerializeField] private Button _button;

    private void OnEnable()
    {
        _button.onClick.AddListener(OpenScene);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OpenScene);
    }

    public void SetScene(string sceneName)
    {
        _sceneName = sceneName;    
    } 

    private void OpenScene()
    {
        if (!string.IsNullOrEmpty(_sceneName))
        {
            SceneManager.LoadScene(_sceneName);
        }
        else
        {
            Debug.LogWarning("Scene name is empty! Укажи сцену в инспекторе.");
        }
    }
}