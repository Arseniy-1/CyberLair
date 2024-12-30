using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena : MonoBehaviour
{
    [SerializeField] private Queue<Wave> _waves;
}

public class Wave : MonoBehaviour
{
    [SerializeField] private float _timer;

    public event Action WaveRaised;

    public void Work()
    {
        
    }
}
