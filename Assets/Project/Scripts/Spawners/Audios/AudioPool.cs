namespace Project.Scripts.Spawners.Audios
{
    public class AudioPool : Pool<Audio>
    {
        private readonly AudioFabric _audioFabric;
        
        public AudioPool(AudioFabric fabric, Audio prefab, int startAmount) 
            : base(prefab, startAmount)
        {
            _audioFabric = fabric;
        }

        protected override Audio Create()
        {
            Audio enemy = _audioFabric.Create(Prefab);

            enemy.gameObject.SetActive(false);

            return enemy;
        }
    }
}