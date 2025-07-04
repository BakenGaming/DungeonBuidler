using UnityEngine;

public class EnemyHandler : MonoBehaviour, IHandler, IDamageable
{
    [SerializeField] private EnemyStatsSO enemyStatsSO;
    private HealthSystem healthSystem;
    private StatSystem statSystem;
    void Start()
    {
        Initialize();
    }

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

    public void HandleDeath()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(int _damage)
    {
        healthSystem.LoseHealth(_damage);
        if(healthSystem.GetCurrentHealth() <= 0)
            HandleDeath();
    }

    public HealthSystem GetHealthSystem() { return healthSystem; }

    public StatSystem GetStatSystem() { return statSystem; }
}
