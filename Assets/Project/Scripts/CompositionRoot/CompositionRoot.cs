using System.Collections.Generic;
using System.Linq;
using Project.Scripts.Arena;
using UnityEngine;

namespace Project.Scripts.CompositionRoot
{
    public class CompositionRoot : MonoBehaviour
    {
        [SerializeField] List<WaveConfig> _wavesConfigs;
        
        private Arena.Arena _arena;
        private EnemyFabric _fabric;

        private void Start()
        {
            _arena = Instantiate(new GameObject("Arena").AddComponent<Arena.Arena>());
            _fabric = Instantiate(new GameObject("EnemyFabric").AddComponent<EnemyFabric>());
            
            var waves = new Queue<Wave>(_wavesConfigs.Select(config => new Wave(config, _fabric)));

            _arena.Initialize(waves);
            _arena.Work();
        }
    }
}