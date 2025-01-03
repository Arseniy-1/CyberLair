using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.ArenaSystem
{
    public class Arena : MonoBehaviour
    {
        private Queue<Wave> _waves;
        
        public event Action WavesDone;
        
        public void Initialize(Queue<Wave> waves)
        {
            _waves = waves;
        }

        public void Work()
        {
            StartNewWave();
        }

        private void StartNewWave()
        {
            if (_waves.Count == 0)
            {
                WavesDone?.Invoke();
                
                return;
            }
            
            Wave wave = _waves.Dequeue();

            wave.OnWaveFinished += HandleWavesEnd;
            wave.Begin();
            
        }

        private void HandleWavesEnd(Wave wave)
        {
            wave.OnWaveFinished -= HandleWavesEnd;
            
            StartNewWave();
        }
    }
}