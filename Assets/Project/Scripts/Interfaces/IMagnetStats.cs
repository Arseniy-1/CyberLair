using Project.Scripts.Stats;

namespace Project.Scripts.Interfaces
{
    public interface IMagnetStats
    {
        public MagnetRange MagnetRange { get; }
        public MagnetForce MagnetForce { get; }
    }
}