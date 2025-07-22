using System.Threading;
using Cysharp.Threading.Tasks;
using Project.Scripts.Interfaces;
using Project.Scripts.MessageBroker;
using Project.Scripts.MessageBroker.EnemyMessageBrokers;
using Project.Scripts.SkillSystem.SkillSOClasses;
using Project.Scripts.Stats;
using UniRx;

namespace Project.Scripts.SkillSystem.SkillInstances
{
    public class ArtayaShield : ISkillInstance
    {
        private readonly float _shieldRepairAmount;
    
        private readonly ShieldAmount _shield;
        
        public ArtayaShield(SkillData skillData, ArtayaShieldSkill skill, CancellationToken token)
        {
            _shield = skillData.PlayerStats.ShieldAmount;
            _shieldRepairAmount = skill.ShieldRepairAmount;
        
            MessageBrokerHolder.Enemy
                .Receive<M_EnemyDeath>()
                .Subscribe(_ => HandleEnemyDeath())
                .AddTo(token);
        }

        public void Disable() { }
        
        private void HandleEnemyDeath()
        {
            _shield.RepairShield(_shieldRepairAmount);
        }
    }
}