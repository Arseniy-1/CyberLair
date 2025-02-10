using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEngine;

namespace Project.Scripts.Servises
{
    public class OrbitalHandler
    {
        private readonly List<Orbital> _orbitals = new();

        public void AddOrbital(Orbital orbital, Transform holder)
        {
            _orbitals.Add(orbital);

            DistributeEqually(holder);
        }
        
        private void DistributeEqually(Transform holder)
        {
            if (_orbitals.IsNullOrEmpty()) return;

            int count = _orbitals.Count;
            float angleStep = 360f / count;

            for (int i = 0; i < count; i++)
            {
                var currentBoomerang = _orbitals[i];
            
                float angle = i * angleStep;
                Vector3 position = CalculatePosition(angle, holder);
            
                currentBoomerang.transform.position = position;
                currentBoomerang.Initialize(holder);
            }
        }
    
        private Vector3 CalculatePosition(float angle, Transform holder)
        {
            float radians = angle * Mathf.Deg2Rad;

            float x = Mathf.Cos(radians);
            float y = Mathf.Sin(radians);
        
            Vector3 localPosition = new Vector3(x, y, 0);
            return holder.position + localPosition;
        }
    }
}