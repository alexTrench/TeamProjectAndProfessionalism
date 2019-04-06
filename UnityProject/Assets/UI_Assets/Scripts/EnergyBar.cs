using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBar : MonoBehaviour
{
    private EnergySystem energySystem;
    public  GameObject   energyBar;

    public void Setup(EnergySystem energySystem)
    {
        this.energySystem = energySystem;
        
        energySystem.OnEnergyChanged += EnergySystem_OnEnergyChanged;
    }

    private void EnergySystem_OnEnergyChanged(object sender, System.EventArgs e)
    {
        energyBar.transform.localScale = new Vector3(energySystem.GetEnergyPercent(), 1);
    }
}
