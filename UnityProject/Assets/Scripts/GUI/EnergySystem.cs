using System;

public class EnergySystem
{
    // public event that deals with changes of the value of energy
    public event EventHandler OnEnergyChanged;

    // local private variables that hold energy and max energy
    private float energy;
    private float energyMAX;

    // initialise function for the local private variables
    public EnergySystem(float energyMAX)
    {
        this.energyMAX = energyMAX;
        energy = energyMAX;
    }

    // Get Energy
    public float GetEnergy()
    {
        return energy;
    }

    // Get Max Energy
    public float GetMaxEnergy()
    {
        return energyMAX;
    }

    // Get Energy Percent
    public float GetEnergyPercent()
    {
        return energy / energyMAX;
    }

    public void setEnergy(float newEnergy)
    {
        energy = newEnergy;
        OnEnergyChanged?.Invoke(this, EventArgs.Empty);
    }

    public void setMaxEnergy(float newMaxEnergy)
    {
        energyMAX = newMaxEnergy;
        OnEnergyChanged?.Invoke(this, EventArgs.Empty);
    }

    // Decrease Energy by desired amount hold inside a parameter
    public void DecreaseEnergy(float decreaseAmount)
    {
        energy -= decreaseAmount;
        if (energy < 0) energy = 0;
        OnEnergyChanged?.Invoke(this, EventArgs.Empty);
    }

    // Increase Energy by desired amount hold inside a parameter
    public void RegenerateEnergy(float regenerateAmount)
    {
        energy += regenerateAmount;
        if (energy > energyMAX) energy = energyMAX;
        OnEnergyChanged?.Invoke(this, EventArgs.Empty);
    }
}
