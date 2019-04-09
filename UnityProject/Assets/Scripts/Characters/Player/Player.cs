using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : BaseCharacter
{
    private PlayerController       m_playerController;
    private bool                   m_isPlayerControlled = false;
    private NavMeshAgent           m_nav;
    private CharacterManagerScript m_characterManager;
    private ZombieManagerScript    m_zombieManager;
    private Transform              m_lookAtTransform;
    private Inventory              m_inventory;
    
    [SerializeField] private float m_distanceThreshold = 5.0f;
    [SerializeField] private float m_navMeshRadius     = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_playerController = GetComponent<PlayerController>();
        m_nav = GetComponent<NavMeshAgent>();

        // Get CharacterManagerScript
        m_characterManager = GameObject.FindGameObjectWithTag("CharacterManager").GetComponent<CharacterManagerScript>();
        Debug.Assert(m_characterManager != null, "Could not find CharacterManagerScript!");

        // Get ZombieManagerScript
        m_zombieManager = GameObject.FindGameObjectWithTag("ZombieManager").GetComponent<ZombieManagerScript>();
        Debug.Assert(m_zombieManager != null, "Could not find ZombieManagerScript!");

        // Get Inventory script
        m_inventory = GetComponent<Inventory>();
        Debug.Assert(m_inventory != null, "Could not find Inventory script!");

        // Enable animation script
        GetComponent<PlayerAnimationScript>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if dead
        if (m_isDead)
        {
            m_nav.enabled = false;
            m_playerController.enabled = false;
            return;
        }

        // Update player character AI
        if (!m_isPlayerControlled)
        {
            // If the player character is too far away from user-controlled 
            // player character, get closer 
            if (GetDistanceToControlledPlayer() > m_distanceThreshold)
                m_nav.SetDestination(GetRandomNavMeshLocation());

            // Look at nearest zombie target
            if (m_zombieManager.GetNumOfZombies() > 0)
                m_lookAtTransform = GetNearestZombie().transform;

            transform.LookAt(m_lookAtTransform);

            // Shoot at nearest target
            if (m_zombieManager.GetNumOfZombies() > 0)
                FireWeapon();
        }
    }

    // Returns true if this player character is controlled by the user
    public bool IsPlayerControlled()
    {
        return m_isPlayerControlled;
    }

    // Sets whether or not the player is controlled by the user
    public void SetIsPlayerControlled(bool isPlayerControlled)
    {
        m_isPlayerControlled = isPlayerControlled;
    }

    // Revives the player character
    public void Revive()
    {
        m_isDead = false;
        m_health = m_maxHealth;
    }

    // Returns the nearest zombie character
    private Zombie GetNearestZombie()
    {
        float distance = float.MaxValue;
        int index = 0;

        // Get and store all zombie characters
        List<Zombie> zombies = m_zombieManager.GetZombieCharacters();

        // Find nearest zombie character
        for (int i = 0; i < zombies.Count; i++)
        {
            float distanceToZombie = (this.transform.position - zombies[i].transform.position).magnitude;
            if (distanceToZombie < distance)
            {
                distance = distanceToZombie;
                index = i;
            }
        }

        return zombies[index];
    }

    // Returns the distance between this player and the user-controlled player character
    private float GetDistanceToControlledPlayer()
    {
        // Get currently controlled player
        Player controlledPlayer = m_characterManager.GetCurrentPlayer();

        // Check if this is currently controlled character
        if (this == controlledPlayer)
            return 0.0f;

        // Return distance betweem
        return (this.transform.position - controlledPlayer.transform.position).magnitude;
    }

    // Returns a random position within a sphere of "m_navMeshRadius" where the 
    // currently controlled player character is the centre of the sphere
    private Vector3 GetRandomNavMeshLocation()
    {
        Vector3 randomDirection = Random.insideUnitSphere * m_navMeshRadius;
        randomDirection += m_characterManager.GetCurrentPlayer().transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, m_navMeshRadius, 1))
            finalPosition = hit.position;

        return finalPosition;
    }

    // Fires the equipped weapon. NOTE: Only called by AI controlled character
    private void FireWeapon()
    {
        for (int i = 0; i < m_inventory.slots.Length; i++)
        {
            if (m_inventory.isActive[i])
            {
                int weaponID = m_inventory.slots[i].gameObject.transform.GetComponentInChildren<ItemId>().itemId;

                switch (weaponID)
                {
                    // AK-47
                    case 0:
                        GetComponentInChildren<AK_Script>().Fire();
                        break;

                    // Shotgun
                    case 1:
                        GetComponentInChildren<ShotgunScript>().Fire();
                        break;

                    // Rocket launcher
                    case 2:
                        GetComponentInChildren<RPG_Script>().Fire();
                        break;

                    // M4
                    case 3:
                        GetComponentInChildren<M4_Script>().Fire();
                        break;

                    default:
                        break;
                }
            }
        }
    }
}
