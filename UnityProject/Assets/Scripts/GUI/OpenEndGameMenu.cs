using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class OpenEndGameMenu : MonoBehaviour
{
    // variable which is accesible from inside the editor in order to link scene objects
    [SerializeField] private GameObject endGameUI = null; // visual representation of the pause menu

    private static bool isEndGame;
    private int numberOfDead;

    // characters manager
    private CharacterManagerScript charactersManager;

    // Start is called before the first frame update
    void Start()
    {
        // look on the list of objects and get the component for character manager script
        charactersManager = GameObject.FindGameObjectWithTag("CharacterManager").GetComponent<CharacterManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        // if it is not yet End Game (players are still alive)
        if(!isEndGame)
        {
            // loop through each individual player
            for (int index = 0; index <= 3; index++)
            {
                // initialise number of dead players each frame
                numberOfDead = 0;

                // and check whether or not his health has reached 0 or below
                if (charactersManager.GetPlayerByIndex(index).GetHealth() <= 0.0f)
                {
                    // count the dead people
                    numberOfDead++;
                    Debug.Log("Character " + index + " has died. Number of dead people: " + numberOfDead);
                }
            }
        }

        // if all the people are dead
        if(numberOfDead == 4)
        {
            // activate End Game Menu
            isEndGame = true;
            ActivateEndMenu();
        }
    }

    void ActivateEndMenu()
    {
        // pause the time, make pause menu active and set the cursor to active
        Time.timeScale = 0.0f;
        endGameUI.SetActive(true);
        Cursor.visible = true;
    }

    public static bool IsEndGame() => isEndGame;
}
