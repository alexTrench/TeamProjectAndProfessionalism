using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    // variables which are accesible from inside the editor in order to link scene objects
    [SerializeField] private GameObject energyBar     = null; // the UI element that represents energy bar
    [SerializeField] private Text       energyText    = null; // the UI Text element for the visual number
    [SerializeField] private Text       maxEnergyText = null; // UI Text element for the Max energy

    // private energy system that controls the visual logic of the bar
    private EnergySystem energySystem;

    // initialise a bar using an EnergySystem object as parameter
    public void Setup(EnergySystem energySystem)
    {
        this.energySystem = energySystem;

        // OnEnergyChanges is an EventHandler and it activates the function below
        energySystem.OnEnergyChanged += EnergySystem_OnEnergyChanged;
    }

    private void EnergySystem_OnEnergyChanged(object sender, System.EventArgs e)
    {
        // update the visual representation of the bar and the UI text as well
        energyBar.transform.localScale = new Vector3(energySystem.GetEnergyPercent(), 1);
        energyText.text                = ((int)energySystem.GetEnergy()).ToString();
        maxEnergyText.text             = ((int)energySystem.GetMaxEnergy()).ToString();
    }
}
