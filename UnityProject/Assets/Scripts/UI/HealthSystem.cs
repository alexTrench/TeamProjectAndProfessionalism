using System;

public class HealthSystem
{
    public event EventHandler OnHealthChanged;

    private float health;
    private float healthMAX;

    public HealthSystem(float healthMAX)
    {
        this.healthMAX = healthMAX;
        health = healthMAX;
    }

    public float GetHealth()
    {
        return health;
    }

    public float GetMaxHealth()
    {
        return healthMAX;
    }

    public float GetHealthPercent()
    {
        return health / healthMAX;
    }

    public void setHealth(float newHealth)
    {
        health = newHealth;
        OnHealthChanged?.Invoke(this, EventArgs.Empty);
    }

    public void setHealthMax(float newHealthMax)
    {
        healthMAX = newHealthMax;
    }

    public void Damage(float damageAmount)
    {
        health -= damageAmount;
        if(health < 0) health = 0;
        OnHealthChanged?.Invoke(this, EventArgs.Empty);
    }

    public void Heal(float healAmount)
    {
        health += healAmount;
        if (health > healthMAX) health = healthMAX;
        OnHealthChanged?.Invoke(this, EventArgs.Empty);
    }
}
