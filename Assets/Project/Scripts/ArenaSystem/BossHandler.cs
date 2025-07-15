using System.Threading;
using Cysharp.Threading.Tasks;
using Project.Scripts.EnemySystem;
using Project.Scripts.MessageBroker;
using Project.Scripts.MessageBroker.EnemyMessageBrokers;
using Project.Scripts.Stats.View;
using UniRx;
using UnityEngine;

namespace Project.Scripts.ArenaSystem
{
    public class BossHandler : MonoBehaviour
    {
        [SerializeField] [Header("Prefabs")] private Cage _cagePrefab;
        [SerializeField] private BossChest _bossChestPrefab;
        
        [SerializeField] [Header("BossViews")] private StatsBar _bossHealthBar;
        [SerializeField] private StatsText _bossHealthText;
        
        private Cage _cageInstance;
        private BossChest _bossChestInstance;
        private Enemy _bossInstance;
        
        private Transform _playerTransform;

        private void OnDisable()
        {
            if (_bossInstance)
                _bossInstance.OnDestroyed -= HandleBossDeath;
        }

        public void Initialize(Transform playerTransform, CancellationToken token)
        {
            _playerTransform = playerTransform;
            
            _bossHealthBar.gameObject.SetActive(false);
            
            MessageBrokerHolder.Enemy
                .Receive<M_BossSpawned>()
                .Subscribe(message => HandleBossSpawn(message.Boss))
                .AddTo(token);
            
            MessageBrokerHolder.Chest
                .Receive<M_ChestRaised>()
                .Subscribe(_ => HandleChestRaised())
                .AddTo(token);
        }

        private void HandleBossSpawn(Enemy boss)
        {
            _bossInstance = boss;
            _bossInstance.OnDestroyed += HandleBossDeath;
            
            _cageInstance ??= Instantiate(_cagePrefab);

            _cageInstance.gameObject.SetActive(true);
            _cageInstance.transform.position = _playerTransform.position;
            
            _bossHealthBar.Initialize(_bossInstance.EnemyStats.Health);
            _bossHealthText.Initialize(_bossInstance.EnemyStats.Health);
            
            _bossHealthBar.gameObject.SetActive(true);
            
            MessageBrokerHolder.Camera
                .Publish(new M_CameraZoomOut());
        }
        
        private void HandleBossDeath(Enemy enemy)
        {
            _bossInstance.OnDestroyed -= HandleBossDeath;
            _bossChestInstance ??= Instantiate(_bossChestPrefab);

            _bossChestInstance.gameObject.SetActive(true);
            _bossChestInstance.transform.position = enemy.transform.position;
            
            _cageInstance.gameObject.SetActive(false);
            _bossHealthBar.gameObject.SetActive(false);

            MessageBrokerHolder.Camera
                .Publish(new M_CameraZoomIn());
        }

        private void HandleChestRaised()
        {
            _bossChestInstance.gameObject.SetActive(false);
        }
    }
}