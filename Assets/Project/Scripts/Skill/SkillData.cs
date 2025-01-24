public class SkillData
{
    public SkillData(WeaponHolder weaponHolder, PlayerStats playerStats, PlayerStats startPlayerStats, int level)
    {
        WeaponHolder = weaponHolder;
        StartPlayerStats = startPlayerStats;
        PlayerStats = playerStats;
        Level = level;
    }

    public WeaponHolder WeaponHolder { get; private set;}
    public PlayerStats StartPlayerStats { get; private set;}
    public PlayerStats PlayerStats{ get; private set;}
    public int Level { get; private set;}
}