using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniHealthHandler : MonoBehaviour
{
    // variables which are accesible from inside the editor in order to link scene objects
    [SerializeField] private HealthSystem healthSystem = null; // the health system logic for the bar
    [SerializeField] private HealthBar    healthBar    = null; // the GUI representation of the health bar
    [SerializeField] private int          playerIndex  = 0;    // current player in use
    
    // characters manager
    private CharacterManagerScript charactersManager;

    // updated player health variables
    private float uPlayerHealth;
    private float uPlayerMaxHealth;
    private int   uPlayerIndex;

    // current player health
    private float playerHealth;
    private float playerMaxHealth;
    
    // Start is called before the first frame update
    void Start()
    {
        // look on the list of objects and get the component for character manager script
        charactersManager = GameObject.FindGameObjectWithTag("CharacterManager").GetComponent<CharacterManagerScript>();

        // get health/max health of current player in use
        playerHealth    = charactersManager.GetPlayerByIndex(playerIndex).GetHealth();
        playerMaxHealth = charactersManager.GetPlayerByIndex(playerIndex).GetMaxHealth();

        // initialise a system and format the bar using the system's logic
        healthSystem = new HealthSystem(playerHealth);
        healthBar.Setup(healthSystem);
    }

    private void Update()
    {
        // update the current player index and its health
        uPlayerHealth = charactersManager.GetPlayerByIndex(playerIndex).GetHealth();
        uPlayerMaxHealth = charactersManager.GetPlayerByIndex(playerIndex).GetMaxHealth();

        if(playerMaxHealth != uPlayerMaxHealth)
        {
            if (!charactersManager.GetPlayerByIndex(playerIndex).IsDead())
            {
                healthSystem.setHealth(uPlayerHealth);
                healthSystem.setHealthMax(uPlayerMaxHealth);
                healthBar.Setup(healthSystem);
            }
        }

        if (uPlayerHealth != playerHealth)
        {
            // decrease the visual aspect of the bar and update the player's health
            //healthSystem.Damage(playerHealth - uPlayerHealth);
            healthSystem.setHealth(uPlayerHealth);
            playerHealth = uPlayerHealth;
        }
    }
}
