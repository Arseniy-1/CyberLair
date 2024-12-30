using UnityEngine;

namespace Project.Scripts.CompositionRoot
{
    public class EnemyFabric : MonoBehaviour
    {
        public Enemy Create(Enemy enemy)
        {
            return enemy;
        }
    }
}