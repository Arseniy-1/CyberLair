using UniRx;
using UnityEngine;

[CreateAssetMenu(fileName = "ArtayaShieldSkill", menuName = "Skill/Simple/ArtayaShield", order = 51)]
public class ArtayaShieldSkill : Skill
{
    private CompositeDisposable _disposable;

    public override void Apply(SkillData skillData)
    {
        if (_disposable != null)
            _disposable.Dispose();
        
        _disposable = new CompositeDisposable();
        MessageBrokerHolder.Enemy.Receive<M_Enemy_Death>().Subscribe((message) => HandleEnemyDeath())
            .AddTo(_disposable);
    }

    private void OnDestroy()
    {
        _disposable.Dispose();
    }

    private void HandleEnemyDeath()
    {
        Debug.Log("HandleEnemyDeath");
    }
}
