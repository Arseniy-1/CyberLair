public interface ISummonMoveStats : IMoverStats
{
    Speed Speed { get; }
    float MoveRadius { get; }
    float MoveDelay { get; }
}