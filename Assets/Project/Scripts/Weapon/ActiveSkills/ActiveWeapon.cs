using UnityEngine;
using UnityEngine.Serialization;

namespace Project.Scripts.Weapon.ActiveSkills
{
    public class ActiveWeapon : MonoBehaviour
    {
        [SerializeField] protected int Damage;
        [SerializeField] protected float ActionRadius;

        protected Transform TargetTransform { get; private set; }

        public void Initialize(float actionRadius, Transform targetTransform)
        {
            ActionRadius = actionRadius;
            TargetTransform = targetTransform;
        }
    }
}