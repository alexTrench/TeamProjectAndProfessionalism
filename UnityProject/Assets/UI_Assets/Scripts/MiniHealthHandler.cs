using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniHealthHandler : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private HealthBar    healthBar;
    [SerializeField] private int          playerIndex;

    // characters manager
    private CharacterManagerScript charactersManager;

    // updated player health variables
    private float uPlayerHealth;
    private int   uPlayerIndex;

    // current player health
    private float playerHealth;
    
    // Start is called before the first frame update
    void Start()
    {
        // look on the list of objects and get the component for character manager script
        charactersManager = GameObject.FindGameObjectWithTag("CharacterManager").GetComponent<CharacterManagerScript>();

        playerHealth = charactersManager.GetPlayerByIndex(playerIndex).GetHealth();

        healthSystem = new HealthSystem(playerHealth);
        healthBar.Setup(healthSystem);
    }

    private void Update()
    {
        // update the current player index and its health
        uPlayerHealth = charactersManager.GetPlayerByIndex(playerIndex).GetHealth();

        if (uPlayerHealth < playerHealth)
        {
            // decrease the visual aspect of the bar and update the player's health
            healthSystem.Damage(playerHealth - uPlayerHealth);
            playerHealth = uPlayerHealth;
        }
    }
}
