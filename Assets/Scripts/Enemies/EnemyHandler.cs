using UnityEngine;

public class EnemyHandler : MonoBehaviour, IHandler
{
    [SerializeField] private EnemyStatsSO enemyStatsSO;
    private HealthSystem healthSystem;
    private StatSystem statSystem;

    public void Initialize()
    {
        healthSystem = new HealthSystem(enemyStatsSO.health);
        statSystem = new StatSystem(enemyStatsSO);
    }

    public void UpdateHealth(int amount)
    {
        healthSystem.LoseHealth(amount);
        if (healthSystem.GetCurrentHealth() <= 0)
        {
            HandleDeath();
        }
    }

    public HealthSystem GetHealthSystem() { return healthSystem; }

    public StatSystem GetStatSystem() { return statSystem; }
    public void HandleDeath()
    {
        Destroy(gameObject);
    }
}
