public interface ISummonMoveStats : IMoverStats
{
    float Speed { get; }
    float MoveRadius { get; }
    float MoveDelay { get; }
}