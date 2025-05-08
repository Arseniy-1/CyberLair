using UnityEngine;
using UnityEngine.UI;
using YG;

public class RewardedAdOpener : MonoBehaviour
{
    [SerializeField] private Button _rewardedAdButton;
    [SerializeField] private RewardedAdType _rewardedAdType;
    
    private void OnEnable()
    {
        _rewardedAdButton.onClick.AddListener(OpenRewardedAd);
    }

    private void OnDisable()
    {
        _rewardedAdButton.onClick.RemoveListener(OpenRewardedAd);
    }
    
    private void OpenRewardedAd()
    {
        YandexGame.RewVideoShow((int)_rewardedAdType);
    }
}