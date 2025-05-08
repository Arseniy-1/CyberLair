using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class TutorialWindow : Window
{
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _previousButton;
    [SerializeField] private Button _finishButton;

    [SerializeField] private List<Window> _windows;

    private int _currentWindowIndex = 0;

    public event Action OnFinished;

    private void OnEnable()
    {
        _nextButton.onClick.AddListener(OnNextButtonClick);
        _previousButton.onClick.AddListener(OnPreviousButtonClick);
        _finishButton.onClick.AddListener(FinishTutorial);
    }

    private void OnDisable()
    {
        _nextButton.onClick.RemoveListener(OnNextButtonClick);
        _previousButton.onClick.RemoveListener(OnPreviousButtonClick);
        _finishButton.onClick.RemoveListener(FinishTutorial);
    }

    private void OnNextButtonClick()
    {
        _currentWindowIndex += 1;

        OpenWindow(_currentWindowIndex);
    }

    private void OnPreviousButtonClick()
    {
        _currentWindowIndex -= 1;

        OpenWindow(_currentWindowIndex);
    }

    private void OpenWindow(int index)
    {
        if (index < 0 || index >= _windows.Count)
            return;

        foreach (Window window in _windows)
            window.gameObject.SetActive(false);

        _windows[index].gameObject.SetActive(true);

        if (_currentWindowIndex == _windows.Count - 1)
        {
            _nextButton.gameObject.SetActive(false);
            _finishButton.gameObject.SetActive(true);
        }
        else
        {
            _nextButton.gameObject.SetActive(true);
            _finishButton.gameObject.SetActive(false);
        }
        
        if (_currentWindowIndex < 1)
            _previousButton.gameObject.SetActive(false);
        else
            _previousButton.gameObject.SetActive(true);
    }

    private void FinishTutorial()
    {
        OnFinished?.Invoke();
    }
}