namespace StateMashineSytem
{
    public interface IStateSwitcher
    {
        void SwitchState<T>() where T : IState;
    }
}
