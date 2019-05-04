using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyHandler : MonoBehaviour
{
    // variables which are accesible from inside the editor in order to link scene objects
    [SerializeField] private EnergySystem energySystem = null; // the energy system which controls energy
    [SerializeField] private EnergyBar    energyBar    = null; // visual representation of the energy bar

    // local characters manager
    private CharacterManagerScript charactersManager;

    // list of all player's health
    private float energy;
    private float maxEnergy;

    // updated player energy and max energy
    private float updatedEnergy;
    private float updatedMaxEnergy;

    // Start is called before the first frame update
    void Start()
    {
        // look up on the list of objects and get CharacterManagerScript component for the object tagged as CharacterManager
        charactersManager = GameObject.FindGameObjectWithTag("CharacterManager").GetComponent<CharacterManagerScript>();

        // initialise energy system
        energySystem = new EnergySystem(100.0f);

        // and setup the bar using the system
        energyBar.Setup(energySystem);
    }

    private void Update()
    {
        // update the current player's index, health and max health
        updatedMaxEnergy = charactersManager.GetMaxEnergy();
        updatedEnergy = charactersManager.GetEnergy();

        if (maxEnergy < updatedMaxEnergy)
        {
            maxEnergy = updatedMaxEnergy;
            energySystem.setEnergy(updatedEnergy);
            energySystem.setMaxEnergy(updatedMaxEnergy);
            energyBar.Setup(energySystem);
        }

        if (updatedEnergy < energy)
        {
            // decrease the visual aspect of the bar and update the player's health
            energySystem.DecreaseEnergy(updatedMaxEnergy - updatedEnergy);
            energy = updatedEnergy;
        }

        if(updatedEnergy > energy)
        {
            energySystem.RegenerateEnergy(updatedMaxEnergy - updatedEnergy);
        }
    }
}
