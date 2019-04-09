using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniHealthHandler : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private HealthBar    healthBar;
    [SerializeField] private int          playerIndex;
    
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

        playerHealth    = charactersManager.GetPlayerByIndex(playerIndex).GetHealth();
        playerMaxHealth = charactersManager.GetPlayerByIndex(playerIndex).GetMaxHealth();

        healthSystem = new HealthSystem(playerHealth);
        healthBar.Setup(healthSystem);
    }

    private void Update()
    {
        // update the current player index and its health
        uPlayerHealth = charactersManager.GetPlayerByIndex(playerIndex).GetHealth();
        uPlayerMaxHealth = charactersManager.GetPlayerByIndex(playerIndex).GetMaxHealth();

        if (uPlayerHealth < playerHealth)
        {
            // decrease the visual aspect of the bar and update the player's health
            healthSystem.Damage(playerHealth - uPlayerHealth);
            playerHealth = uPlayerHealth;
        }
    }
}
