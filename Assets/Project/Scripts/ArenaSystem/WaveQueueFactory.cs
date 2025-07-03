using System.Collections.Generic;
using System.Linq;
using Project.Scripts.ArenaSystem;

public class WaveQueueFactory
{
    public Queue<Wave> Create(List<WaveConfig> configs, MainEnemySpawner mainEnemySpawner)
    {
        return new Queue<Wave>(configs
            .Select(config => new Wave(config, mainEnemySpawner))
            .ToList());
    }
}