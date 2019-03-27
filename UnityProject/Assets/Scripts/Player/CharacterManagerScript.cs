using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManagerScript : MonoBehaviour
{
    public BaseCharacter m_character1;
    public BaseCharacter m_character2;
    public BaseCharacter m_character3;
    public BaseCharacter m_character4;

    public FollowTarget followingCamera;

    private List<BaseCharacter> m_playerCharacters;
    private int m_currentPlayerIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_playerCharacters = new List<BaseCharacter>();

        if (m_character1 != null) m_playerCharacters.Add(m_character1);
        if (m_character2 != null) m_playerCharacters.Add(m_character2);
        if (m_character3 != null) m_playerCharacters.Add(m_character3);
        if (m_character4 != null) m_playerCharacters.Add(m_character4);

        Debug.Assert(m_playerCharacters.Count > 0, "No players added to the CharacterManager!");

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
        
        followingCamera.setTarget(m_playerCharacters[m_currentPlayerIndex].gameObject.transform);
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

        followingCamera.setTarget(m_playerCharacters[m_currentPlayerIndex].gameObject.transform);
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
                playerCharacter.GetComponent<PlayerAnimationScript>().enabled = true;
            }
            // Disable other character
            else
            {
                playerCharacter.GetComponent<CharacterController>().enabled = false;
                playerCharacter.GetComponent<PlayerController>().enabled = false;
                playerCharacter.GetComponent<PlayerAnimationScript>().enabled = false;
            }
        }
    }
    
    BaseCharacter GetCurrentPlayer()
    {
        return m_playerCharacters[m_currentPlayerIndex];
    }

    public bool AreAllPlayersDead()
    {
        foreach (var playerCharacter in m_playerCharacters)
        {
            if (!playerCharacter.IsDead())
                return false;
        }

        return true;
    }
}
