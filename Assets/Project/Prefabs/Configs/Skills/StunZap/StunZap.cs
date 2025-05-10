using System;
using Project.Prefabs.Configs.Skills.StunZap;
using System.Collections.Generic;
using Project.Prefabs.Configs.Skills.Durability;
using Project.Scripts.EnemySystem;

[Serializable]
public class StunZap : ISkillInstance
{
    private float _stunDuration;

    private SkillData _date;
    
    private List<Bullet> _subscribedBullets = new List<Bullet>();
    
    public StunZap(SkillData skillData, StunZapSkill skill)
    {
        _date = skillData;
        _stunDuration = skill.StunDuration;

        _date.WeaponHolder.Weapon.Shot += InnerSubscribe;
    }
    
    public void Disable()
    {
        _date.WeaponHolder.Weapon.Shot -= InnerSubscribe;

        foreach (var bullet in _subscribedBullets)
            bullet.OnDamagableCollided -= StunEnemy;
    }
    
    private void InnerSubscribe(Bullet bullet)
    {
        if(_subscribedBullets.Contains(bullet))
            return;
        
        bullet.OnDamagableCollided += StunEnemy;
        _subscribedBullets.Add(bullet);
    }

    private void StunEnemy(IDamageable damageable)
    {
        if (damageable is Enemy enemy && Enum.IsDefined(typeof(BossTypes), (BossTypes)enemy.EnemyType))
            return;
        
        (damageable as IStunable)?.TakeStun(_stunDuration);
    }
}
