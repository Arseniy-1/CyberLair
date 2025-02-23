using UniRx;

namespace Project.Prefabs.Configs.Skills.ArtayaShield
{
    public class ArtayaShield : SkillInstance
    {
        private readonly float _shieldRepairAmount;
    
        private readonly ShieldAmount _shield;
        private readonly CompositeDisposable _disposable;
        
        public ArtayaShield(SkillData skillData, ArtayaShieldSkill skill)
        {
            _shield = skillData.PlayerStats.ShieldAmount;
            _shieldRepairAmount = skill.ShieldRepairAmount;
        
            _disposable = new CompositeDisposable();
            MessageBrokerHolder.Enemy.Receive<M_Enemy_Death>().Subscribe((message) => HandleEnemyDeath())
                .AddTo(_disposable);
        }

        public override void Disable()
        {
            _disposable.Dispose();
        }
        
        private void HandleEnemyDeath()
        {
            _shield.RepairShield(_shieldRepairAmount);
        }
    }
}