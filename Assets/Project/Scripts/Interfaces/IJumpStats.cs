using Project.Scripts.Stats;

namespace Project.Scripts.Interfaces
{
    public interface IJumpStats
    {
        public JumpSpeed JumpSpeed { get; }
        public JumpTime JumpTime { get; }
        public JumpReloadTime JumpReloadTime { get; }
    }
}