using UnityEngine;

namespace Project.Scripts.EnemySystem.Bosses.PerimeterSentinel
{
    public class ShakePool : Pool<Shake>
    {
        public ShakePool(Shake prefab, int startAmount) : base(prefab, startAmount) { }
        
        protected override Shake Create()
        {
            Shake template = Object.Instantiate(Prefab);
            template.gameObject.SetActive(false);

            return template;
        }
    }
}