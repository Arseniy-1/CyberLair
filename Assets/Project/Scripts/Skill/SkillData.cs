public class SkillData
{
    public SkillData(WeaponHolder weaponHolder, PlayerStats playerStats)
    {
        WeaponHolder = weaponHolder;
        PlayerStats = playerStats;
    }

    public WeaponHolder WeaponHolder { get; private set;}
    public PlayerStats PlayerStats{ get; private set;}
}