using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHandler : MonoBehaviour
{
    public  HealthSystem healthSystem;
    public  HealthBar    healthBar;
    private CharacterManagerScript currentCharacter;
    private float playerHealth = 0;
    private float updatedPlayerHealth;
    private int   currentPlayerIndex;
    private int   updatedPlayerIndex;

    // Start is called before the first frame update
    void Start()
    {
        currentCharacter = GameObject.FindGameObjectWithTag("CharacterManager").GetComponent<CharacterManagerScript>();
        //currentPlayerIndex = currentCharacter.GetCharacterIndex();
        Debug.Log("current player index: " + currentPlayerIndex);
        healthSystem = new HealthSystem(100.0f);
        healthBar.Setup(healthSystem);
    }

    private void Update()
    {
        //updatedPlayerHealth = currentCharacter.GetCurrentPlayer().getHealth();
        //updatedPlayerIndex = currentCharacter.GetCharacterIndex();
        Debug.Log("updated player index: " + updatedPlayerIndex);

        if (playerHealth <= 0 || updatedPlayerIndex != currentPlayerIndex)
        {
            //playerHealth = currentCharacter.GetCurrentPlayer().getHealth();
        }

        if (updatedPlayerHealth < playerHealth)
        {
            healthSystem.Damage(playerHealth - updatedPlayerHealth);
            playerHealth = updatedPlayerHealth;
        }
    }
}
