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
        if (m_playerCharacters.Count <= 1)
            return;

        // Update current player index to character that isn't dead
        if (++m_currentPlayerIndex > m_playerCharacters.Count - 1)
            m_currentPlayerIndex = 0;

        //bool isCharacterDead = true;
        //int loopCounter = 0;

        //do
        //{
        //    if (++m_currentPlayerIndex > m_playerCharacters.Count - 1)
        //        m_currentPlayerIndex = 0;

        //    isCharacterDead = m_playerCharacters[m_currentPlayerIndex].IsDead();
        //    loopCounter++;
        //}
        //while (isCharacterDead);

        // Enable current character and disable others
        EnableCharacter(m_playerCharacters[m_currentPlayerIndex]);
        
        followingCamera.setTarget(m_playerCharacters[m_currentPlayerIndex].gameObject.transform);
    }

    // Switches to previous character
    void SwitchPreviousCharacter()
    {
        if (m_playerCharacters.Count <= 1)
            return;

        // Update current player index
        if (--m_currentPlayerIndex < 0)
            m_currentPlayerIndex = m_playerCharacters.Count - 1;

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

    // Remove this?
    BaseCharacter GetCurrentPlayer()
    {
        return m_playerCharacters[m_currentPlayerIndex];
    }
}
