using System.Threading;
using Cysharp.Threading.Tasks;
using Project.Scripts.Interfaces;
using Project.Scripts.MessageBroker;
using Project.Scripts.MessageBroker.EnemyMessageBrokers;
using Project.Scripts.SkillSystem.SkillSOClasses;
using Project.Scripts.SkillSystem.SkillViews;
using Project.Scripts.Spawners.LandMines;
using UniRx;

namespace Project.Scripts.SkillSystem.SkillInstances
{
    public class NonStop : ISkillInstance
    {
        private readonly int _neededDiedEnemyCount;
        private int _currentDiedEnemyCount;

        private readonly LandMineSpawner _landMineSpawner;
        private readonly SkillData _data;

        public NonStop(SkillData skillData, NonStopSkill skill, CancellationToken token)
        {
            _data = skillData;

            _currentDiedEnemyCount = 0;
            _neededDiedEnemyCount = skill.NeededDiedEnemyCount;
            _landMineSpawner = new LandMineSpawner(skill.LandMinePrefab);

            MessageBrokerHolder.Enemy
                .Receive<M_EnemyDeath>()
                .Subscribe(_ => HandleEnemyDeath())
                .AddTo(token);

            _data.PlayerJumper.JumpPerformed += OnJumpPerformed;
        }

        public void Disable()
        {
            _data.PlayerJumper.JumpPerformed -= OnJumpPerformed;
        }

        private void HandleEnemyDeath()
        {
            _currentDiedEnemyCount++;
        }

        private void OnJumpPerformed()
        {
            if (_currentDiedEnemyCount < _neededDiedEnemyCount)
                return;

            _currentDiedEnemyCount = 0;
            LandMine mine = _landMineSpawner.Spawn();
            mine.transform.position = _data.PlayerJumper.transform.position;
        }
    }
}