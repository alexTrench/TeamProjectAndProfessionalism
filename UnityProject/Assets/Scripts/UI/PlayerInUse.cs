using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInUse : MonoBehaviour
{
    [SerializeField] private List<Text> players = null;
    [SerializeField] private List<Text> playersHealth = null;
    [SerializeField] private List<Text> playersHealthDelimiter = null;
    [SerializeField] private List<Text> playersMaxHealth = null;
    [SerializeField] private Text currentPlayerInfo = null;

    // characters manager
    private CharacterManagerScript charactersManager;

    private int currentPlayerIndex;
    private int updatedPlayerIndex;

    // Start is called before the first frame update
    void Start()
    {
        // look on the list of objects and get the component for character manager script
        charactersManager = GameObject.FindGameObjectWithTag("CharacterManager").GetComponent<CharacterManagerScript>();

        currentPlayerIndex = charactersManager.GetCurrentPlayerIndex();

        currentPlayerInfo.text = (currentPlayerIndex + 1).ToString();

        players[currentPlayerIndex].color                = new Color(1f, 0f, 0f, 0.5882353f);
        playersHealth[currentPlayerIndex].color          = new Color(1f, 0f, 0f, 0.5882353f);
        playersHealthDelimiter[currentPlayerIndex].color = new Color(1f, 0f, 0f, 0.5882353f);
        playersMaxHealth[currentPlayerIndex].color       = new Color(1f, 0f, 0f, 0.5882353f);
    }

    private void Update()
    {
        updatedPlayerIndex = charactersManager.GetCurrentPlayerIndex();

        if(updatedPlayerIndex != currentPlayerIndex)
        {
            currentPlayerInfo.text = (updatedPlayerIndex + 1).ToString();

            players[currentPlayerIndex].color = new Color(0f, 0f, 0f, 1f);
            players[updatedPlayerIndex].color = new Color(1f, 0f, 0f, 0.5882353f);
       
            playersHealth[currentPlayerIndex].color = new Color(0f, 0f, 0f, 1f);
            playersHealth[updatedPlayerIndex].color = new Color(1f, 0f, 0f, 0.5882353f);

            playersHealthDelimiter[currentPlayerIndex].color = new Color(0f, 0f, 0f, 1f);
            playersHealthDelimiter[updatedPlayerIndex].color = new Color(1f, 0f, 0f, 0.5882353f);

            playersMaxHealth[currentPlayerIndex].color = new Color(0f, 0f, 0f, 1f);
            playersMaxHealth[updatedPlayerIndex].color = new Color(1f, 0f, 0f, 0.5882353f);

            currentPlayerIndex = updatedPlayerIndex;
        }

    }
}
