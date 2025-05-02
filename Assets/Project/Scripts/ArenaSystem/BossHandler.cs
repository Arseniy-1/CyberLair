using DG.Tweening;
using Project.Scripts.EnemySystem;
using Project.Scripts.MessageBroker.EnemyMessageBrokers;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

namespace Project.Scripts.ArenaSystem
{
    public class BossHandler : MonoBehaviour
    {
        [SerializeField, MinMaxSlider(9f, 20f), Header("Camera")] private Vector2 _cameraZoomSize;
        [SerializeField] private float _zoomDuration;
        
        [SerializeField, Header("Prefabs")] private Cage _cagePrefab;
        [SerializeField] private BossChest _bossChestPrefab;
        
        private readonly CompositeDisposable _disposable = new();
        private Cage _cageInstance;
        private BossChest _bossChestInstance;
        
        private Camera _mainCamera;
        private Transform _playerTransform;

        public void Initialize(Transform playerTransform)
        {
            _playerTransform = playerTransform;
            _mainCamera = Camera.main;
            _mainCamera.orthographicSize = _cameraZoomSize.x;
            
            MessageBrokerHolder.Enemy.Receive<M_BossSpawned>().Subscribe(message => HandleBossSpawn())
                .AddTo(_disposable);
            
            MessageBrokerHolder.Enemy.Receive<M_BossDeath>().Subscribe(message => HandleBossDeath(message.Boss))
                .AddTo(_disposable);
            
            MessageBrokerHolder.Chest.Receive<M_ChestRaised>().Subscribe(message => HandleChestRaised())
                .AddTo(_disposable);
        }

        private void HandleBossSpawn()
        {
            _cageInstance ??= Instantiate(_cagePrefab);

            _cageInstance.gameObject.SetActive(true);
            _cageInstance.transform.position = _playerTransform.position;
            
            DOTween.To(() => _mainCamera.orthographicSize, x => _mainCamera.orthographicSize = x, _cameraZoomSize.y, _zoomDuration)
                .SetEase(Ease.InOutSine);
        }
        
        private void HandleBossDeath(Enemy enemy)
        {
            _bossChestInstance ??= Instantiate(_bossChestPrefab);

            _bossChestInstance.gameObject.SetActive(true);
            _bossChestInstance.transform.position = enemy.transform.position;
            
            _cageInstance.gameObject.SetActive(false);
            
            DOTween.To(() => _mainCamera.orthographicSize, x => _mainCamera.orthographicSize = x, _cameraZoomSize.x, _zoomDuration)
                .SetEase(Ease.InOutSine);
        }

        private void HandleChestRaised()
        {
            _bossChestInstance.gameObject.SetActive(false);
        }
    }
}