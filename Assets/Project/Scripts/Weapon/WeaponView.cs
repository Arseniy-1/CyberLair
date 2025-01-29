using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Serialization;

public class WeaponView : MonoBehaviour
{
    [SerializeField] private Image[] _bulletIcons;
    [SerializeField] private Sprite _emptyBulletSprite;
    [SerializeField] private Sprite _fullBulletSprite;
    [SerializeField] private Image _reloadSpinner;
    
    [SerializeField] private IncrementalReloadWeapon _weapon;

    [SerializeField] private float _blinkDelay = 0.15f;
    private Coroutine _blinkCoroutine;
    private WaitForSeconds _blinkWait;
    
    private void OnEnable()
    {
        _blinkWait = new WaitForSeconds(_blinkDelay);
        _weapon.OnAmmoUpdated += UpdateAmmoUI;
    }

    private void OnDisable()
    {
        _weapon.OnAmmoUpdated -= UpdateAmmoUI;
    }
    
    private void UpdateAmmoUI(int currentAmmo, int maxAmmo)
    {
        for (int i = 0; i < _weapon.MagazineSize; i++)
        {
            if (i < currentAmmo)
            {
                _bulletIcons[i].sprite = _fullBulletSprite;
                _bulletIcons[i].gameObject.SetActive(true);
            }
            else
            {
                _bulletIcons[i].sprite = _emptyBulletSprite;
                _bulletIcons[i].gameObject.SetActive(true);
            }
        }

        for (int i = currentAmmo; i < _bulletIcons.Length; i++)
        {
            _bulletIcons[i].gameObject.SetActive(false);
        }
        
        if (_blinkCoroutine != null)
        {
            StopCoroutine(_blinkCoroutine);
            _blinkCoroutine = null;
        }

        if (_weapon.IsReloading && currentAmmo < maxAmmo)
        {
            int nextBulletIndex = currentAmmo;
            _blinkCoroutine = StartCoroutine(BlinkBullet(nextBulletIndex));
        }
        
        if (_weapon.IsReloading)
        {
            StartReloadSpinner();
        }
        else
        {
            StopReloadSpinner();
        }
    }

    private IEnumerator BlinkBullet(int bulletIndex)
    {
        _bulletIcons[bulletIndex].gameObject.SetActive(true);

        while (_weapon.IsReloading)
        {
            _bulletIcons[bulletIndex].sprite = _emptyBulletSprite;
            yield return _blinkWait;
            
            _bulletIcons[bulletIndex].sprite = _fullBulletSprite;
            yield return _blinkWait;
        }

        _bulletIcons[bulletIndex].sprite = _fullBulletSprite;
    }

    private void StartReloadSpinner()
    {
        if (_reloadSpinner != null)
        {
            _reloadSpinner.gameObject.SetActive(true);
            _reloadSpinner.transform.DORotate(new Vector3(0, 0, 360f), 1f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart);
        }
    }

    private void StopReloadSpinner()
    {
        if (_reloadSpinner != null)
        {
            _reloadSpinner.gameObject.SetActive(false);
            _reloadSpinner.DOKill();
        }
    }
}
