using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInUse : MonoBehaviour
{
    // variables which are accesible from inside the editor in order to link scene objects
    [SerializeField] private List<Text> players                = null; // the list of players on the scene
    [SerializeField] private List<Text> playersHealth          = null; // the list of player's health
    [SerializeField] private List<Text> playersHealthDelimiter = null; // the list of player's health delimiter
    [SerializeField] private List<Text> playersMaxHealth       = null; // the list of player's max health
    [SerializeField] private Text currentPlayerInfo            = null; // UI Text element that represents current player in use

    // local private characters manager
    private CharacterManagerScript charactersManager;

    // current/updated player's index
    private int currentPlayerIndex;
    private int updatedPlayerIndex;

    // Start is called before the first frame update
    void Start()
    {
        // look on the list of objects and get 'CharacterManagerScript' component for object tagged as 'CharacterManager'
        charactersManager = GameObject.FindGameObjectWithTag("CharacterManager").GetComponent<CharacterManagerScript>();

        // initialise the current player's index
        currentPlayerIndex = charactersManager.GetCurrentPlayerIndex();

        // initialise the UI Text element to the current player in use
        currentPlayerInfo.text = (currentPlayerIndex + 1).ToString();

        // initialise the colours scheme for current player in use
        players[currentPlayerIndex].color                = new Color(1f, 0f, 0f, 0.5882353f);
        playersHealth[currentPlayerIndex].color          = new Color(1f, 0f, 0f, 0.5882353f);
        playersHealthDelimiter[currentPlayerIndex].color = new Color(1f, 0f, 0f, 0.5882353f);
        playersMaxHealth[currentPlayerIndex].color       = new Color(1f, 0f, 0f, 0.5882353f);
    }

    private void Update()
    {
        // update the player's index each frame
        updatedPlayerIndex = charactersManager.GetCurrentPlayerIndex();

        // if the player's index has changed
        if(updatedPlayerIndex != currentPlayerIndex)
        {
            // update the current player's index
            currentPlayerInfo.text = (updatedPlayerIndex + 1).ToString();

            // update the colours scheme for the updated player in use
            players[currentPlayerIndex].color = new Color(0f, 0f, 0f, 0.4901961f);
            players[updatedPlayerIndex].color = new Color(1f, 0f, 0f, 0.5882353f);
       
            playersHealth[currentPlayerIndex].color = new Color(0f, 0f, 0f, 0.4901961f);
            playersHealth[updatedPlayerIndex].color = new Color(1f, 0f, 0f, 0.5882353f);

            playersHealthDelimiter[currentPlayerIndex].color = new Color(0f, 0f, 0f, 0.4901961f);
            playersHealthDelimiter[updatedPlayerIndex].color = new Color(1f, 0f, 0f, 0.5882353f);

            playersMaxHealth[currentPlayerIndex].color = new Color(0f, 0f, 0f, 0.4901961f);
            playersMaxHealth[updatedPlayerIndex].color = new Color(1f, 0f, 0f, 0.5882353f);

            currentPlayerIndex = updatedPlayerIndex;
        }

    }
}
