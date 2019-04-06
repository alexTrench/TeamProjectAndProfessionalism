using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHandler : MonoBehaviour
{
    // declaration of the health system and bar;
    public  HealthSystem healthSystem;
    public  HealthBar    healthBar;

    // current player in control
    private CharacterManagerScript charactersManager;
    
    // list of all player's health
    private List<float> playersHealth;

    // updated player health variables
    private float uPlayerHealth;
    private int   uPlayerIndex;

    // current player index
    private int   cPlayerIndex;

    // Start is called before the first frame update
    void Start()
    {
        // look on the list of objects and get the component for character manager script
        charactersManager = GameObject.FindGameObjectWithTag("CharacterManager").GetComponent<CharacterManagerScript>();

        // initialise the list with generic health values for each character
        playersHealth = new List<float> { 100.0f, 100.0f, 100.0f, 100.0f };

        // initialise the current player's index
        cPlayerIndex = charactersManager.GetPlayerIndex();

        healthSystem = new HealthSystem(100.0f);
        healthBar.Setup(healthSystem);
    }

    private void Update()
    {
        // update the current player index and its health
        uPlayerHealth = charactersManager.GetCurrentPlayer().getHealth();
        uPlayerIndex = charactersManager.GetPlayerIndex();

        // if updated index is different
        if (uPlayerIndex != cPlayerIndex)
        {
            // update the current player index,
            // update the health inside the health system and
            // adjust the health bar to the new system 
            cPlayerIndex = uPlayerIndex;
            healthSystem.setHealth(playersHealth[uPlayerIndex]);

            if (uPlayerHealth < playersHealth[uPlayerIndex])
            {
                // decrease the visual aspect of the bar and update the player's health
                healthSystem.Damage(playersHealth[uPlayerIndex] - uPlayerHealth);
                playersHealth[uPlayerIndex] = uPlayerHealth;
            }
            Debug.Log("updated player index: " + uPlayerIndex + "  health: " + playersHealth[uPlayerIndex]);
        }
        else if (uPlayerHealth < playersHealth[cPlayerIndex])
        {
            // decrease the visual aspect of the bar and update the player's health
            healthSystem.Damage(playersHealth[cPlayerIndex] - uPlayerHealth);
            playersHealth[cPlayerIndex] = uPlayerHealth;
        }
    }
}
