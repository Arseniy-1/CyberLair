using UnityEngine;
using System.Collections.Generic;
using Assets.SimpleLocalization.Scripts;
using TMPro;
using UnityEngine.UI;
using YG;

public class MapSelector : MonoBehaviour
{
    [SerializeField] private List<MapData> _maps;

    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private Image _image;

    [SerializeField] private Button _startGameButton;
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _previousButton;

    [SerializeField] private SceneOpener _easySceneOpener;
    [SerializeField] private SceneOpener _hardSceneOpener;

    private int _currentMapIndex = 0;

    private void OnEnable()
    {
        LocalizationManager.Read();
        _image.sprite = _maps[0].MapImage;

        _nextButton.onClick.AddListener(OnNextButtonClick);
        _previousButton.onClick.AddListener(OnPreviousButtonClick);

        HandleButtonClick();
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

        if (_currentMapIndex < 0)
            _currentMapIndex = _maps.Count - 1;

        HandleButtonClick();
    }

    private void HandleButtonClick()
    {
        string developMapName = "DevelopedMap";

        MapData selectedMap = _maps[_currentMapIndex];

        LocalizationManager.Language = YandexGame.lang;
        _name.text = LocalizationManager.Localize(selectedMap.MapNameKey);

        _image.sprite = selectedMap.MapImage;

        _easySceneOpener.SetScene(selectedMap.EasyMap);
        _hardSceneOpener.SetScene(selectedMap.HardMap);

        _startGameButton.interactable = selectedMap.MapNameKey != developMapName;
    }
}