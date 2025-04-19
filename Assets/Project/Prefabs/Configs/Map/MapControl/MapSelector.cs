using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class MapSelector : MonoBehaviour
{
    [SerializeField] private List<MapData> _maps;

    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private Image _image;
    
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _previousButton;
    
    [SerializeField] private SceneOpener _easySceneOpener;
    [SerializeField] private SceneOpener _hardSceneOpener;
    
    private int _currentMapIndex = 0;
    
    private void OnEnable()
    {
        _name.text = _maps[0].MapName;
        _image.sprite = _maps[0].MapImage;
        
        _nextButton.onClick.AddListener(OnNextButtonClick);
        _previousButton.onClick.AddListener(OnPreviousButtonClick);
    }
    
    private void OnDisable()
    {
        _nextButton.onClick.RemoveListener(OnNextButtonClick);
        _previousButton.onClick.RemoveListener(OnPreviousButtonClick);
    }

    private void OnNextButtonClick()
    {
        _currentMapIndex += 1;

        if (_currentMapIndex > _maps.Count - 1)
            _currentMapIndex = 0;

        HandleButtonClick();
    }

    private void OnPreviousButtonClick()
    {
        _currentMapIndex -= 1;
        
        if(_currentMapIndex < 0)
            _currentMapIndex = _maps.Count - 1; 
        
        HandleButtonClick();
    }
    
    private void HandleButtonClick()
    {
        MapData selectedMap = _maps[_currentMapIndex];
        
        _name.text = selectedMap.MapName;
        _image.sprite = selectedMap.MapImage;
        
        _easySceneOpener.SetScene(selectedMap.EasyMap);
        _hardSceneOpener.SetScene(selectedMap.HardMap);
    }
}