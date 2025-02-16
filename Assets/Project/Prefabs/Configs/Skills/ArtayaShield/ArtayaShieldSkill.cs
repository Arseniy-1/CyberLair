using Project.Scripts.EnemySystem;
using UnityEngine;

[CreateAssetMenu(fileName = "ArtayaShieldSkill", menuName = "Skill/Simple/ArtayaShield", order = 51)]
public class ArtayaShieldSkill : Skill
{
    [SerializeField] private KillerShield _killerShield;
    
    public override void Apply(SkillData skillData)
    {
        // _killerShield.;
    }
}

public class KillerShield : MonoBehaviour
{
    [SerializeField] private StatModifier _shieldModifier;
    
    private SkillData _skillData;
    
    public void Initialize(SkillData skillData)
    {
        skillData.WeaponHolder.Weapon.Shooted += OnShooted;
    }

    private void OnShooted(Bullet bullet)
    {
        bullet.OnDamagableCollided += OnDamagableCollided;
    }

    private void OnDamagableCollided(IDamageable damageable)
    {
        
        if(damageable is Enemy enemy)
        {
            enemy.OnDestroyed += OnEnemyDied;
        }
    }

    private void OnEnemyDied(Enemy enemy)
    {
        enemy.OnDestroyed -= OnEnemyDied;
        _skillData.PlayerStats.ShieldAmount.AddModifier(_shieldModifier.Copy());
    }
}