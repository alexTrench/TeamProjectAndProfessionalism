using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterManagerScript : MonoBehaviour
{
    private List<BaseCharacter> m_playerCharacters;
    private int                 m_currentPlayerIndex = 0;
    private FollowTarget        m_followingCamera;

    public BaseCharacter m_character1;
    public BaseCharacter m_character2;
    public BaseCharacter m_character3;
    public BaseCharacter m_character4;

    private void Awake()
    {
        // Add given player characters to list
        m_playerCharacters = new List<BaseCharacter>();
        if (m_character1 != null) m_playerCharacters.Add(m_character1);
        if (m_character2 != null) m_playerCharacters.Add(m_character2);
        if (m_character3 != null) m_playerCharacters.Add(m_character3);
        if (m_character4 != null) m_playerCharacters.Add(m_character4);
        Debug.Assert(m_playerCharacters.Count > 0, "No players added to the CharacterManager!");

        // Init following camera
        m_followingCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FollowTarget>();
        Debug.Assert(m_followingCamera != null, "Could not find FollowTarget component!");

        // Set initial player controlled character
        EnableCharacter(m_playerCharacters[m_currentPlayerIndex]);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (InputManager.PreviousCharacter())
            SwitchPreviousCharacter();
        else if (InputManager.NextCharacter())
            SwitchNextCharacter();
    }

    // Switches to next character
    void SwitchNextCharacter()
    {
        // Get and store num of characters
        int numCharacters = m_playerCharacters.Count;

        if (numCharacters <= 1)
            return;

        // Switch to next character that isn't dead
        int currentIndex = m_currentPlayerIndex;

        do
        {
            if (++currentIndex > numCharacters - 1)
                currentIndex = 0;

            if (currentIndex == m_currentPlayerIndex)
                break;
        }
        while (m_playerCharacters[currentIndex].IsDead());

        m_currentPlayerIndex = currentIndex;

        // Enable current character and disable others
        EnableCharacter(m_playerCharacters[m_currentPlayerIndex]);
        
        m_followingCamera.setTarget(m_playerCharacters[m_currentPlayerIndex].gameObject.transform);
    }

    // Switches to previous character
    void SwitchPreviousCharacter()
    {
        // Get and store num of characters
        int numCharacters = m_playerCharacters.Count;

        if (numCharacters <= 1)
            return;

        // Switch to next character that isn't dead
        int currentIndex = m_currentPlayerIndex;

        do
        {
            if (--currentIndex < 0)
                currentIndex = numCharacters - 1;

            if (currentIndex == m_currentPlayerIndex)
                break;

        }
        while (m_playerCharacters[currentIndex].IsDead());

        m_currentPlayerIndex = currentIndex;

        // Enable current character and disable others
        EnableCharacter(m_playerCharacters[m_currentPlayerIndex]);

        m_followingCamera.setTarget(m_playerCharacters[m_currentPlayerIndex].gameObject.transform);
    }

    // Enables the given character and disables the other characters
    void EnableCharacter(BaseCharacter character)
    {
        foreach (var playerCharacter in m_playerCharacters)
        {
            // Enable current character
            if (character == playerCharacter)
            {
                playerCharacter.GetComponent<CharacterController>().enabled = true;
                playerCharacter.GetComponent<PlayerController>().enabled = true;
                playerCharacter.GetComponent<NavMeshAgent>().enabled = false;
                playerCharacter.GetComponent<Player>().SetIsPlayerControlled(true);
           
    
            }
            // Disable other character
            else
            {
                playerCharacter.GetComponent<CharacterController>().enabled = false;
                playerCharacter.GetComponent<PlayerController>().enabled = false;
                playerCharacter.GetComponent<NavMeshAgent>().enabled = true;
                playerCharacter.GetComponent<Player>().SetIsPlayerControlled(false);
                
            }
        }
    }
    
    // Returns the currently controlled player character
    public BaseCharacter GetCurrentPlayer()
    {
        return m_playerCharacters[m_currentPlayerIndex];
    }

    // Returns true if all player characters are dead
    public bool AreAllPlayersDead()
    {
        foreach (var playerCharacter in m_playerCharacters)
        {
            if (!playerCharacter.IsDead())
                return false;
        }

        return true;
    }

    // Returns a list of all player characters
    public List<BaseCharacter> GetPlayerCharacters()
    {
        return m_playerCharacters;
    }

    public int GetPlayerIndex()
    {
        return m_currentPlayerIndex;
    }
}
