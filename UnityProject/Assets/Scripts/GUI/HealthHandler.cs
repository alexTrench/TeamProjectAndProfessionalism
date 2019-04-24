using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHandler : MonoBehaviour
{
    // variables which are accesible from inside the editor in order to link scene objects
    [SerializeField] private HealthSystem healthSystem = null; // the health system logic for the bar
    [SerializeField] private HealthBar    healthBar    = null; // GUI representation of the health bar

    // local characters manager
    private CharacterManagerScript charactersManager;
    
    // list of all player's health
    private List<float> playersHealth;
    private List<float> playersMaxHealth;

    // updated player health and index
    private float uPlayerHealth;
    private int   uPlayerIndex;

    // updated player health and index
    private float uMaxPlayerHealth;

    // current player index
    private int   cPlayerIndex;

    // Start is called before the first frame update
    void Start()
    {
        // look up on the list of objects and get CharacterManagerScript component for the object tagged as CharacterManager
        charactersManager = GameObject.FindGameObjectWithTag("CharacterManager").GetComponent<CharacterManagerScript>();

        // initialise the list with basic health values for each character
        playersHealth    = new List<float> { 100.0f, 100.0f, 100.0f, 100.0f };
        playersMaxHealth = new List<float> { 100.0f, 100.0f, 100.0f, 100.0f };

        // initialise the current player's index
        cPlayerIndex = charactersManager.GetCurrentPlayerIndex();

        // initialise health system and setup a health bar
        healthSystem = new HealthSystem(100.0f);
        healthBar.Setup(healthSystem);
    }

    private void Update()
    {
        // update the current player's index, health and max health
        uMaxPlayerHealth = charactersManager.GetCurrentPlayer().GetMaxHealth();
        uPlayerHealth = charactersManager.GetCurrentPlayer().GetHealth();
        uPlayerIndex  = charactersManager.GetCurrentPlayerIndex();

        

        // if updated index is different
        if (uPlayerIndex != cPlayerIndex)
        {
            // update the current player index,
            // update the health inside the health system and
            // adjust the health bar to the new system 
            cPlayerIndex = uPlayerIndex;

            if (playersMaxHealth[uPlayerIndex] != uMaxPlayerHealth)
            {
                playersMaxHealth[uPlayerIndex] = uMaxPlayerHealth;
                if (!charactersManager.GetPlayerByIndex(uPlayerIndex).IsDead())
                {
                    healthSystem.setHealth(uPlayerHealth);
                    healthSystem.setHealthMax(uMaxPlayerHealth);
                    healthBar.Setup(healthSystem);
                }
            }
            else
            {
                healthSystem.setHealth(playersHealth[uPlayerIndex]);
            }

            if (uPlayerHealth < playersHealth[uPlayerIndex])
            {
                // decrease the visual aspect of the bar and update the player's health
                healthSystem.Damage(playersHealth[uPlayerIndex] - uPlayerHealth);
                playersHealth[uPlayerIndex] = uPlayerHealth;
            }
        }
        else
        {
            if (playersMaxHealth[uPlayerIndex] != uMaxPlayerHealth)
            {
                playersMaxHealth[uPlayerIndex] = uMaxPlayerHealth;
                if (!charactersManager.GetCurrentPlayer().IsDead())
                {
                    healthSystem.setHealth(uPlayerHealth);
                    healthSystem.setHealthMax(uMaxPlayerHealth);
                    healthBar.Setup(healthSystem);
                }
            }

            if (uPlayerHealth < playersHealth[cPlayerIndex])
            {
                // decrease the visual aspect of the bar and update the player's health
                healthSystem.Damage(playersHealth[cPlayerIndex] - uPlayerHealth);
                playersHealth[cPlayerIndex] = uPlayerHealth;
            }
        }
    }
}
