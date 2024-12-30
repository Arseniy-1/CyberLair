using System;

public interface IDestoyable<T>
{
    public event Action<T> OnDestroyed;
}