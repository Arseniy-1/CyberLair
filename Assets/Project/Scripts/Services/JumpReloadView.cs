using UnityEngine;
using UnityEngine.UI;

public class JumpReloadView : MonoBehaviour
{
    [SerializeField] private Jumper jumper;
    private Image _cooldownImage;

    private void Awake()
    {
        _cooldownImage = GetComponent<Image>();
        _cooldownImage.type = Image.Type.Filled;
        _cooldownImage.fillMethod = Image.FillMethod.Radial360;
        _cooldownImage.fillOrigin = (int)Image.Origin360.Top;
        _cooldownImage.fillClockwise = true;
        _cooldownImage.fillAmount = 1f;
    }

    private void OnEnable()
    {
        jumper.JumpPerformed += OnJumpPerformed;
    }

    private void OnDisable()
    {
        jumper.JumpPerformed -= OnJumpPerformed;
    }

    private void Update()
    {
        if (jumper.IsOnCooldown)
        {
            _cooldownImage.fillAmount = jumper.CooldownTimer / jumper.JumpStats.JumpReloadTime.CurrentValue;
        }
    }

    private void OnJumpPerformed()
    {
        _cooldownImage.fillAmount = 0f;
    }
}