using Project.Prefabs.Configs.Skills.Durability;
using UniRx;

public class NonStop : ISkillInstance
{
    private int _nedeedDiedEnemyCount;
    private int _currentDiedEnemyCount;

    private LandMineSpanwer _landMineSpawner;

    private SkillData _data;

    private readonly CompositeDisposable _disposable;

    public NonStop(SkillData skillData, NonStopSkill skill)
    {
        _data = skillData;

        _currentDiedEnemyCount = 0;
        _nedeedDiedEnemyCount = skill.NedeedDiedEnemyCount;
        _landMineSpawner = new LandMineSpanwer(skill.LandMinePrefab);

        _disposable = new CompositeDisposable();
        MessageBrokerHolder.Enemy.Receive<M_Enemy_Death>().Subscribe((message) => HandleEnemyDeath())
            .AddTo(_disposable);

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
        if (_currentDiedEnemyCount < _nedeedDiedEnemyCount)
            return;

        _currentDiedEnemyCount = 0;
        var mine = _landMineSpawner.Spawn();
        mine.transform.position = _data.PlayerJumper.transform.position;
    }
}