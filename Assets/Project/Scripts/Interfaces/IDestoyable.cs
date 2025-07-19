using System;

namespace Project.Scripts.Interfaces
{
    public interface IDestoyable<T>
    {
        public event Action<T> OnDestroyed;
    }
}