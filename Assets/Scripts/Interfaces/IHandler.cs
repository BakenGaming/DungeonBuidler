using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHandler
{
    public void Initialize();
    public abstract void UpdateHealth(int _amount);
    public abstract void HandleDeath();
    public abstract HealthSystem GetHealthSystem();
    public abstract StatSystem GetStatSystem();
}
