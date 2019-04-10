using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyHandler : MonoBehaviour
{

    public EnergySystem energySystem;
    public EnergyBar energyBar;

    // Start is called before the first frame update
    void Start()
    {
        energySystem = new EnergySystem(100.0f);

        energyBar.Setup(energySystem);
    }

    private void Update()
    {

    }
}
