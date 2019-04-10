using System;

public class EnergySystem
{
    public event EventHandler OnEnergyChanged;

    private float energy;
    private float energyMAX;

    public EnergySystem(float energyMAX)
    {
        this.energyMAX = energyMAX;
        energy = energyMAX;
    }

    public float GetEnergy()
    {
        return energy;
    }

    public float GetEnergyPercent()
    {
        return energy / energyMAX;
    }

    public void DecreaseEnergy(float decreaseAmount)
    {
        energy -= decreaseAmount;
        if (energy < 0) energy = 0;
        OnEnergyChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RegenerateEnergy(float regenerateAmount)
    {
        energy += regenerateAmount;
        if (energy > energyMAX) energy = energyMAX;
        OnEnergyChanged?.Invoke(this, EventArgs.Empty);
    }
}
