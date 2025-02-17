using System.Collections;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public GameObject _shieldView; // Объект для отображения щита
    public float _cooldown = 15f; // Время перезарядки щита
    private bool _isActive = false; // Активен ли щит в данный момент

    void Start()
    {
        _shieldView.SetActive(false); // Скрыть визуализацию щита при старте
        StartCoroutine(ShieldCooldown());
    }

    public void ActivateShield()
    {
        _isActive = true;
        _shieldView.SetActive(true); // Включаем визуализацию щита
    }

    public void DeactivateShield()
    {
        _isActive = false;
        _shieldView.SetActive(false); // Выключаем визуализацию щита
        StartCoroutine(ShieldCooldown()); // Начинаем перезарядку
    }

    private IEnumerator ShieldCooldown()
    {
        yield return new WaitForSeconds(_cooldown); // Ждем время перезарядки
        ActivateShield(); // Активируем щит
    }

    // Пример взаимодействия с врагом
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyProjectile")) // Проверяем столкновение с вражеским снарядом
        {
            if (_isActive)
            {
                DeactivateShield();
                Destroy(collision.gameObject);
            }
        }
    }
}