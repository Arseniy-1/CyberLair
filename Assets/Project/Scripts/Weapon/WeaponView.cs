using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WeaponView : MonoBehaviour
{
    [SerializeField] private Image[] _bulletIcons;
    [SerializeField] private Sprite _emptyBulletSprite;
    [SerializeField] private Sprite _fullBulletSprite;

    [SerializeField] private IncrementalReloadWeapon _weapon;

    [SerializeField] private Color _clearColor;
    [SerializeField] private Color _emptyBulletColor = new (1f, 1f, 1f, 0.2f);

    [SerializeField] private float _blinkDelay = 0.15f;
    
    private Coroutine _blinkCoroutine;
    private WaitForSeconds _blinkWait;
    private Tween _rotateTween;
    
    private void OnEnable()
    {
        _blinkWait = new WaitForSeconds(_blinkDelay);
        _weapon.OnAmmoUpdated += UpdateAmmoView;
    }

    private void OnDisable()
    {
        _weapon.OnAmmoUpdated -= UpdateAmmoView;

        if (_blinkCoroutine == null)
            return;
        
        StopCoroutine(_blinkCoroutine);
        _blinkCoroutine = null;
    }

    private void UpdateAmmoView(int ammoCount, int maxAmmoCount)
    {
        for (int i = 0; i < _bulletIcons.Length; i++)
        {
            _bulletIcons[i].sprite = _fullBulletSprite;

            if (i < ammoCount)
            {
                _bulletIcons[i].gameObject.SetActive(true);
                _bulletIcons[i].color = _clearColor;
            }
            else if (i >= ammoCount && i < maxAmmoCount)
            {
                _bulletIcons[i].gameObject.SetActive(true);
                _bulletIcons[i].color = _emptyBulletColor;
            }
            else if(i >= maxAmmoCount)
            {
                _bulletIcons[i].gameObject.SetActive(false);
            }
        }

        if (_blinkCoroutine != null)
        {
            StopCoroutine(_blinkCoroutine);
            _blinkCoroutine = null;
        }

        if (!_weapon.IsReloading || ammoCount >= maxAmmoCount) 
            return;

        _blinkCoroutine = StartCoroutine(BlinkBullet(ammoCount));
    }

    private IEnumerator BlinkBullet(int bulletIndex)
    {
        _bulletIcons[bulletIndex].gameObject.SetActive(true);
        _bulletIcons[bulletIndex].color = _clearColor;

        while (_weapon.IsReloading)
        {
            _bulletIcons[bulletIndex].sprite = _emptyBulletSprite;
            yield return _blinkWait;

            _bulletIcons[bulletIndex].sprite = _fullBulletSprite;
            yield return _blinkWait;
        }

        _bulletIcons[bulletIndex].sprite = _fullBulletSprite;
    }
}