public class SkillData
{
    public SkillData(WeaponHolder weaponHolder, PlayerConfig playerConfig, PlayerStats playerStats, int level)
    {
        WeaponHolder = weaponHolder;
        PlayerConfig = playerConfig;
        PlayerStats = playerStats;
        Level = level;
    }

    public WeaponHolder WeaponHolder { get; private set;}
    public PlayerConfig PlayerConfig { get; private set;}
    public PlayerStats PlayerStats{ get; private set;}
    public int Level { get; private set;}
}