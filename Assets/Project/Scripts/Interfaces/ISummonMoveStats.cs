namespace Project.Scripts.Interfaces
{
    public interface ISummonMoveStats : IMoverStats
    {
        public float MoveRadius { get; }
        public float MoveDelay { get; }
    }
}