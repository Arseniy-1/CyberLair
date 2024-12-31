namespace EntityStateMashine
{
    public interface IStateSwitcher
    {
        void SwitchState<T>() where T : IState;
    }
}
