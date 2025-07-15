namespace Project.Scripts.Interfaces
{
    public interface IStateSwitcher
    {
        public void SwitchState<T>() 
            where T : IState;
    }
}
