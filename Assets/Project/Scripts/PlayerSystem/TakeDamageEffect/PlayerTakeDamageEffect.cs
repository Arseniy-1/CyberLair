using Project.Scripts.Services.Enum;
using Project.Scripts.Services.Extensions;
using UnityEngine;

namespace Project.Scripts.PlayerSystem.TakeDamageEffect
{
    public class PlayerTakeDamageEffect : MonoBehaviour
    {
        [SerializeField] private ShakeID _shakeID = ShakeID.Medium;
        [SerializeField] private AudioID _damageSound = AudioID.PlayerTakeDamage;
        [SerializeField] private Player _player;
        [SerializeField] private EntityDamageView _entityDamageView;
        [SerializeField] private LowPassCutoffer _lowPassCutoffer;

        private void Awake()
        {
            _entityDamageView.Initialize();
        }
        
        private void OnEnable()
        {
            _player.OnTakeDamage += HandleTakeDamage;
        }
        
        private void OnDisable()
        {
            _player.OnTakeDamage -= HandleTakeDamage;
            
            _entityDamageView.EndBlink();
            _lowPassCutoffer.CancelCutoff();
        }

        private void HandleTakeDamage()
        {
            _damageSound.Play();
            _shakeID.Shake();

            _entityDamageView.StartBlink();
            _lowPassCutoffer.StartCutoff();
        }
    }
}