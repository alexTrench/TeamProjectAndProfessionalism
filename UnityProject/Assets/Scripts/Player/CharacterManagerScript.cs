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

        for (int i = 0; i < m_playerCharacters.Count; ++i)
        {
            // Enable current player
            if (i == 0)
            {
                BaseCharacter player = m_playerCharacters[m_currentPlayerIndex];
                player.GetComponent<CharacterController>().enabled = true;
                player.GetComponent<PlayerController>().enabled = true;
                player.GetComponent<PlayerAnimationScript>().enabled = true;
            }
            // Disable all other player characters
            else
            {
                BaseCharacter player = m_playerCharacters[i];
                player.GetComponent<CharacterController>().enabled = false;
                player.GetComponent<PlayerController>().enabled = false;
                player.GetComponent<PlayerAnimationScript>().enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
            SwitchCharacter();
    }

    void SwitchCharacter()
    {
        
        if (m_playerCharacters.Count <= 1)
            return;

        // Update current player index
        if (++m_currentPlayerIndex > m_playerCharacters.Count - 1)
            m_currentPlayerIndex = 0;

        for (int i = 0; i < m_playerCharacters.Count; ++i)
        {
            // Enable current player
            if (m_currentPlayerIndex == i)
            {
                BaseCharacter player = m_playerCharacters[m_currentPlayerIndex];
                player.GetComponent<CharacterController>().enabled = true;
                player.GetComponent<PlayerController>().enabled = true;
                player.GetComponent<PlayerAnimationScript>().enabled = true;
            }
            // Disable all other player characters
            else
            {
                BaseCharacter player = m_playerCharacters[i];
                player.GetComponent<CharacterController>().enabled = false;
                player.GetComponent<PlayerController>().enabled = false;
                player.GetComponent<PlayerAnimationScript>().enabled = false;
            }
        }

        followingCamera.setTarget(m_playerCharacters[m_currentPlayerIndex].gameObject.transform);
    }

    BaseCharacter GetCurrentPlayer()
    {
        return m_playerCharacters[m_currentPlayerIndex];
    }
}
