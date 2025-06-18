using System.Threading;
using Cysharp.Threading.Tasks;
using Project.Prefabs.Configs.Skills.Durability;
using UniRx;

public class NonStop : ISkillInstance
{
    private readonly int _neededDiedEnemyCount;
    private int _currentDiedEnemyCount;

    private readonly LandMineSpanwer _landMineSpawner;
    private readonly SkillData _data;

    public NonStop(SkillData skillData, NonStopSkill skill, CancellationToken token)
    {
        _data = skillData;

        _currentDiedEnemyCount = 0;
        _neededDiedEnemyCount = skill.NedeedDiedEnemyCount;
        _landMineSpawner = new LandMineSpanwer(skill.LandMinePrefab);

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