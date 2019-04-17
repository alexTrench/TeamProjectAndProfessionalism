using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyHandler : MonoBehaviour
{
    // variables which are accesible from inside the editor in order to link scene objects
    [SerializeField] private EnergySystem energySystem = null; // the energy system which controls energy
    [SerializeField] private EnergyBar    energyBar    = null; // visual representation of the energy bar

    // Start is called before the first frame update
    void Start()
    {
        // initialise energy system
        energySystem = new EnergySystem(100.0f);

        // and setup the bar using the system
        energyBar.Setup(energySystem);
    }

    private void Update()
    {
        // empty
    }
}
