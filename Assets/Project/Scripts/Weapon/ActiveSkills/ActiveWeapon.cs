using UnityEngine;
using UnityEngine.Serialization;

namespace Project.Scripts.Weapon.ActiveSkills
{
    public class ActiveWeapon : MonoBehaviour
    {
        [SerializeField] private int _damage;

        protected int Damage => _damage;
        protected float ActionRadius { get; private set; }
        protected Transform TargetTransform { get; private set; }

        public void Initialize(float actionRadius, Transform targetTransform)
        {
            ActionRadius = actionRadius;
            TargetTransform = targetTransform;
        }
    }
}