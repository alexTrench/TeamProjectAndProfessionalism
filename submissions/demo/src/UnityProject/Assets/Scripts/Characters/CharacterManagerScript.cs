using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterManagerScript : MonoBehaviour
{
    [SerializeField] private Player m_character1 = null;
    [SerializeField] private Player m_character2 = null;
    [SerializeField] private Player m_character3 = null;
    [SerializeField] private Player m_character4 = null;
    [SerializeField] private float  m_energy     = 100.0f;
    [SerializeField] private float  m_maxEnergy  = 100.0f;

    private List<Player>  m_playerCharacters;
    private int           m_currentPlayerIndex   = 0;
    private FollowTarget  m_followingCamera;
    private Queue<Player> m_deadPlayerCharacters;
    private bool          m_startSwitchTimer     = false;
    private float         m_switchPlayerTimer    = 0.0f;
    private AudioSource   m_audioSource;
    
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

        // Init audio source
        m_audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check for character switch
        if (InputManager.PreviousCharacter())
            SwitchPreviousCharacter();
        else if (InputManager.NextCharacter())
            SwitchNextCharacter();
        else if (InputManager.CharacterHotKey1())
            SwitchToSpecificCharacter(0);
        else if (InputManager.CharacterHotKey2())
            SwitchToSpecificCharacter(1);
        else if (InputManager.CharacterHotKey3())
            SwitchToSpecificCharacter(2);
        else if (InputManager.CharacterHotKey4())
            SwitchToSpecificCharacter(3);

        // Check for dead player characters
        foreach (var player in m_playerCharacters)
        {
            // If player is dead and not already in the queue, then add to queue
            if (player.IsDead() && !m_deadPlayerCharacters.Contains(player))
                m_deadPlayerCharacters.Enqueue(player);
        }

        // If controlled character dies, auto switch to next character after n seconds
        if (GetCurrentPlayer().IsDead())
            m_startSwitchTimer = true;

        if (m_startSwitchTimer)
        {
            m_switchPlayerTimer += Time.deltaTime;
            if (m_switchPlayerTimer >= 3.5f)
            {
                SwitchNextCharacter();
                m_switchPlayerTimer = 0.0f;
                m_startSwitchTimer = false;
            }
        }
            
    }

    /**
     * @brief Allows the player to switch to a specific character.
     * @param characterIndex - The index of the character to be
     *                         switched to.
     */
    void SwitchToSpecificCharacter(int characterIndex) {
        //Check the index is within range.
        if(characterIndex < 0 || characterIndex >= m_playerCharacters.Count) {
            return;
        }
        //Check that the character is not already selected.
        if(m_currentPlayerIndex == characterIndex) {
            return;
        }
        //Check that the character is not dead.
        if(m_playerCharacters[characterIndex].IsDead()) {
            return;
        }

        //Change the player's character.
        m_currentPlayerIndex = characterIndex;

        // Enable current character and disable others
        EnableCharacter(m_playerCharacters[m_currentPlayerIndex]);

        // Play sound
        m_audioSource.Play();

        //Make the camera follow the new character.
        m_followingCamera.setTarget(m_playerCharacters[m_currentPlayerIndex].gameObject.transform);
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

        // Play sound
        m_audioSource.Play();

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

        // Play sound
        m_audioSource.Play();

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
        if (m_deadPlayerCharacters.Count > 0) {
            m_deadPlayerCharacters.Dequeue().Revive();
        }
    }

    /*
     * Player health.
     */

    public void IncrementPlayerHealth(float health)
    {
        foreach (var player in m_playerCharacters)
        {
            player.IncrementHealth(health);
        }
    }

    public void DecrementPlayerHealth(float health)
    {
        foreach (var player in m_playerCharacters)
        {
            player.DecrementHealth(health);
        }
    }

    public void SetPlayerHealth(float health)
    {
        foreach (var player in m_playerCharacters)
        {
            player.SetHealth(health);
        }
    }

    /*
     * Player max health.
     */

    public void IncrementPlayerMaxHealth(float maxHealth)
    {
        foreach (var player in m_playerCharacters)
        {
            player.IncrementMaxHealth(maxHealth);
        }
    }

    public void DecrementPlayerMaxHealth(float maxHealth)
    {
        foreach (var player in m_playerCharacters)
        {
            player.DecrementMaxHealth(maxHealth);
        }
    }

    public void SetPlayerMaxHealth(float maxHealth)
    {
        foreach (var player in m_playerCharacters)
        {
            player.SetHealth(maxHealth);
        }
    }

    /*
     * Player movement speed.
     */

    public void IncrementPlayerMovementSpeed(float speed)
    {
        foreach (var player in m_playerCharacters)
        {
            player.IncrementMovementSpeed(speed);
        }
    }

    public void DecrementPlayerMovementSpeed(float speed)
    {
        foreach (var player in m_playerCharacters)
        {
            player.DecrementMovementSpeed(speed);
        }
    }

    public void SetPlayerMovementSpeed(float speed)
    {
        foreach (var player in m_playerCharacters)
        {
            player.SetMovementSpeed(speed);
        }
    }

    /*
     * Player energy.
     */

    // Getters
    public float GetEnergy() => m_energy;

    public void IncrementPlayerEnergy(float energy)
    {
        m_energy += energy;
        if (m_energy > m_maxEnergy) m_energy = m_maxEnergy;
    }

    public void DecrementPlayerEnergy(float energy)
    {
        m_energy -= energy;
        if (m_energy < 0.0f) m_energy = 0.0f;
    }

    public void SetPlayerEnergy(float energy)
    {
        m_energy = energy;
        if (m_energy > m_maxEnergy) m_energy = m_maxEnergy;
    }

    /*
     * Player max energy.
     */
     
    public float GetMaxEnergy() => m_maxEnergy;

    public void IncrementPlayerMaxEnergy(float maxEnergy) => m_maxEnergy += maxEnergy;

    public void DecrementPlayerMaxEnergy(float maxEnergy)
    {
        m_maxEnergy -= maxEnergy;
        if (m_maxEnergy < 0.0f) m_maxEnergy = 0.0f;
    }

    public void SetMaxEnergy(float maxEnergy) => m_maxEnergy = maxEnergy;
}
