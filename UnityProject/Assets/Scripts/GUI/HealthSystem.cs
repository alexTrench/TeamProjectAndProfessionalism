using System;

public class HealthSystem
{
    // public event that deals with changes of the value of the health
    public event EventHandler OnHealthChanged;

    // private local variables for health and health max
    private float health;
    private float healthMAX;

    // initialise health system using a health max as parameter
    public HealthSystem(float healthMAX)
    {
        this.healthMAX = healthMAX;
        health = healthMAX;
    }
    
    // get current health
    public float GetHealth()
    {
        return health;
    }

    // get max health
    public float GetMaxHealth()
    {
        return healthMAX;
    }

    // get health percentage
    public float GetHealthPercent()
    {
        return health / healthMAX;
    }

    // set health
    public void setHealth(float newHealth)
    {
        health = newHealth;
        OnHealthChanged?.Invoke(this, EventArgs.Empty);
    }

    // set health max
    public void setHealthMax(float newHealthMax)
    {
        healthMAX = newHealthMax;
        OnHealthChanged?.Invoke(this, EventArgs.Empty);
    }

    // deal damage to player
    public void Damage(float damageAmount)
    {
        health -= damageAmount;
        if(health < 0) health = 0;
        OnHealthChanged?.Invoke(this, EventArgs.Empty);
    }

    // heal player
    public void Heal(float healAmount)
    {
        health += healAmount;
        if (health > healthMAX) health = healthMAX;
        OnHealthChanged?.Invoke(this, EventArgs.Empty);
    }
}
