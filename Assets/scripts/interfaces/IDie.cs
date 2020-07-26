using System;

public interface IDie
{
    event Action<IDie> OnDied;
    event Action<int, int> OnHealthChanged;
}

