using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PauseButton : MonoBehaviour
{
    [SerializeField] private Window _pauseWindow;
    [SerializeField] private Window _currentWindow;
    
    [SerializeField] private UnpauseButton _unpauseButton;
    
    private Button _button;

    private void OnEnable()
    {
        _button = GetComponent<Button>();
        
        _button.onClick.AddListener(PauseGame);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(PauseGame);
    }
    
    private void PauseGame()
    {
        _pauseWindow.gameObject.SetActive(true);
        _currentWindow.gameObject.SetActive(false);
        
        MessageBrokerHolder.Game
            .Publish(new M_GamePaused());
        
        _unpauseButton.OnUnpause += ResumeGame;
    }

    private void ResumeGame()
    {
        _pauseWindow.gameObject.SetActive(false);
        _currentWindow.gameObject.SetActive(true);
        
        MessageBrokerHolder.Game
            .Publish(new M_GameUnpaused());
        
        _unpauseButton.OnUnpause += ResumeGame;
    }
}