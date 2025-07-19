using System.Collections.Generic;
using System.Linq;
using Project.Scripts.Spawners.Enemies;

namespace Project.Scripts.ArenaSystem
{
    public class WaveQueueFactory
    {
        public Queue<Wave> Create(List<WaveConfig> configs, MainEnemySpawner mainEnemySpawner)
        {
            List<Wave> waves = configs.Select(config => new Wave(config, mainEnemySpawner)).ToList();
        
            return new Queue<Wave>(waves);
        }
    }
}