using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterManagerScript : MonoBehaviour
{
    [SerializeField] private Player m_character1;
    [SerializeField] private Player m_character2;
    [SerializeField] private Player m_character3;
    [SerializeField] private Player m_character4;

    private List<Player> m_playerCharacters;
    private int m_currentPlayerIndex = 0;
    private FollowTarget m_followingCamera;
    private Queue<Player> m_deadPlayerCharacters;

    private void Awake()
    {
        // Add given player characters to list
        m_playerCharacters = new List<Player>();
        if (m_character1 != null) m_playerCharacters.Add(m_character1);
        if (m_character2 != null) m_playerCharacters.Add(m_character2);
        if (m_character3 != null) m_playerCharacters.Add(m_character3);
        if (m_character4 != null) m_playerCharacters.Add(m_character4);
        Debug.Assert(m_playerCharacters.Count > 0, "No players added to the CharacterManager!");

        // Init queue
        m_deadPlayerCharacters = new Queue<Player>();

        // Init following camera
        m_followingCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FollowTarget>();
        Debug.Assert(m_followingCamera != null, "Could not find FollowTarget component!");

        // Set initial player controlled character
        EnableCharacter(m_playerCharacters[m_currentPlayerIndex]);
    }

    // Update is called once per frame
    void Update()
    {
        // Check for character switch
        if (InputManager.PreviousCharacter())
            SwitchPreviousCharacter();
        else if (InputManager.NextCharacter())
            SwitchNextCharacter();

        // Check for dead player characters
        foreach (var player in m_playerCharacters)
        {
            // If player is dead and not already in the queue, then add to queue
            if (player.IsDead() && !m_deadPlayerCharacters.Contains(player))
                m_deadPlayerCharacters.Enqueue(player);
        }
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
    void EnableCharacter(Player character)
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
                //playerCharacter.GetComponent<WeaponPickup>().enabled = true;
            }
            // Disable other character
            else
            {
                playerCharacter.GetComponent<CharacterController>().enabled = false;
                playerCharacter.GetComponent<PlayerController>().enabled = false;
                playerCharacter.GetComponent<NavMeshAgent>().enabled = true;
                playerCharacter.GetComponent<Player>().SetIsPlayerControlled(false);
                //playerCharacter.GetComponent<WeaponPickup>().enabled = false;
            }
        }
    }

    // Returns the currently controlled player character
    public Player GetCurrentPlayer()
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
    public List<Player> GetPlayerCharacters()
    {
        return m_playerCharacters;
    }

    // Returns the current player index
    public int GetCurrentPlayerIndex()
    {
        return m_currentPlayerIndex;
    }

    // Returns the player character with the given index
    public Player GetPlayerByIndex(int playerIndex)
    {
        Debug.Assert(playerIndex >= 0 && playerIndex < m_playerCharacters.Count, "Invalid player index given!");
        return m_playerCharacters[playerIndex];
    }

    // Revives the player character at the front of the queue
    public void RevivePlayer()
    {
        if (m_deadPlayerCharacters.Count > 0)
            m_deadPlayerCharacters.Dequeue().Revive();
    }
}
