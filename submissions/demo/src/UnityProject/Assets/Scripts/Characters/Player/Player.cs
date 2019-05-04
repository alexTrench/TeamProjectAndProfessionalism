using System;
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
    private CapsuleCollider        m_capsuleCollider;
    private Transform              m_lookAtTransform;
    private Inventory              m_inventory;
    private weaponDatabase         m_weaponDatabase;
    private bool                   m_isReloading        = false;
    private bool                   m_isReviving         = false;
    private float                  m_revivingTimer      = 0.0f;

    [SerializeField] private readonly float m_distanceThreshold  = 5.0f;
    [SerializeField] private readonly float m_navMeshRadius      = 3.5f;

    // AI specific fields
    private readonly float m_maxFiringRangeDistance = 11.0f; // Enemy must be within this distance
    private bool           m_shootAtZombie          = false;
    private readonly float m_fireRateMultiplier     = 35.0f; // Range (fire rate - fire rate * multiplier)
    private float          m_fireDelay              = 1.0f;
    private float          m_delayTimer             = 0.0f;
    private bool           m_isDestinationSet       = false;

    // Start is called before the first frame update
    void Start()
    {
        m_playerController = GetComponent<PlayerController>();
        m_capsuleCollider = GetComponent<CapsuleCollider>();
        m_nav = GetComponent<NavMeshAgent>();

        // Get CharacterManagerScript
        m_characterManager = GameObject.FindGameObjectWithTag("CharacterManager").GetComponent<CharacterManagerScript>();
        Debug.Assert(m_characterManager != null, "Could not find CharacterManagerScript!");

        // Get ZombieManagerScript
        m_zombieManager = GameObject.FindGameObjectWithTag("ZombieManager").GetComponent<ZombieManagerScript>();
        Debug.Assert(m_zombieManager != null, "Could not find ZombieManagerScript!");

        // Get Inventory script
        m_inventory = GetComponent<Inventory>();
        Debug.Assert(m_inventory != null, "Could not find Inventory!");

        // Get WeaponDatabase script
        m_weaponDatabase = GameObject.FindGameObjectWithTag("GameController").GetComponent<weaponDatabase>();
        Debug.Assert(m_weaponDatabase != null, "Could not find WeaponDatabase!");

        // Enable animation script
        GetComponent<PlayerAnimationScript>().enabled = true;

        // Set AI speed to speed set in player controller
        m_nav.speed = m_playerController.GetMovementSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if dead
        if (m_isDead)
        {
            m_nav.enabled = false;
            m_playerController.enabled = false;
            m_capsuleCollider.enabled = false;
            return;
        }

        // Check if reviving
        if (m_isReviving)
        {
            m_revivingTimer += Time.deltaTime;
            if (m_revivingTimer >= 1.2f)
            {
                m_revivingTimer = 0.0f;
                m_isReviving = false;
                m_nav.enabled = true;
            }
        }

        // Update AI
        if (!m_isPlayerControlled)
        {
            UpdateAI();
        }
        // Update player
        else
        {
            UpdatePlayer();
        }

        // Update delay timer (and prevent overflow)
        if (m_delayTimer + Time.deltaTime > float.MaxValue)
            m_delayTimer = m_delayTimer + 1.0f;
        else
            m_delayTimer += Time.deltaTime;
    }

    private void UpdateAI()
    {
        // If the player character is too far away from user-controlled 
        // player character, get closer 
        float distanceToPlayer = GetDistanceToCharacter(m_characterManager.GetCurrentPlayer());

        if (distanceToPlayer > m_distanceThreshold && !m_isDestinationSet)
        {
            if (m_nav.enabled)
            {
                m_nav.SetDestination(m_characterManager.GetCurrentPlayer().transform.position);
                m_nav.isStopped = false;
            }
            m_isDestinationSet = true;
        }

        if (distanceToPlayer <= m_distanceThreshold || IsNavAgentAtDest())
        {
            if (m_nav.enabled)
            {
                m_nav.isStopped = true;
                m_nav.ResetPath();
            }
            m_isDestinationSet = false;
        }
        
        // Look at nearest zombie if within range
        if (m_zombieManager.GetNumOfZombies() > 0 && GetDistanceToCharacter(GetNearestZombie()) <= m_maxFiringRangeDistance)
        {
            m_lookAtTransform = GetNearestZombie().transform;
            m_shootAtZombie = true;
        }
        else
        {
            m_shootAtZombie = false;
        }

        transform.LookAt(m_lookAtTransform);

        // Shoot at nearest target
        if (m_shootAtZombie) FireWeapon();
    }

    private void UpdatePlayer()
    {
        for (int i = 0; i < m_inventory.slots.Length; i++)
        {
            if (m_inventory.isActive[i])
            {
                int weaponID = m_inventory.slots[i].gameObject.transform.GetComponentInChildren<ItemId>().itemId;
                switch (weaponID)
                {
                    case 0:
                        AK_Script ak = GetComponentInChildren<AK_Script>();
                        m_isReloading = ak.GetIsReloading();
                        break;
                    case 1:
                        ShotgunScript shotgun = GetComponentInChildren<ShotgunScript>();
                        m_isReloading = shotgun.GetIsReloading();
                        break;
                    case 2:
                        RPG_Script rpg = GetComponentInChildren<RPG_Script>();
                        //
                        break;
                    case 3:
                        M4_Script m4 = GetComponentInChildren<M4_Script>();
                        m_isReloading = m4.GetIsReloading();
                        break;
                    case 4:
                        HeavyRifleScript heavy = GetComponentInChildren<HeavyRifleScript>();
                        m_isReloading = heavy.GetIsReloading();
                        break;
                    case 5:
                        SciFiPistolScript pistol = GetComponentInChildren<SciFiPistolScript>();
                        m_isReloading = pistol.GetIsReloading();
                        break;
                    case 6:
                        SciFiRifleScript rifle = GetComponentInChildren<SciFiRifleScript>();
                        m_isReloading = rifle.GetIsReloading();
                        break;
                    default:
                        break;
                }
            }
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
        GetComponent<PlayerAnimationScript>().Revive();
        m_capsuleCollider.enabled = true;
        m_isReviving = true;
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

    // Returns the distance between this player and the given character
    private float GetDistanceToCharacter(BaseCharacter character)
    {
        return (this != character) ? (transform.position - character.transform.position).magnitude : 0.0f;
    }
    
    // Returns a random position within a sphere of the given radius and centre point
    private Vector3 GetRandomNavMeshLocation(float sphereRadius, in Vector3 sphereCentre)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * sphereRadius;
        randomDirection += sphereCentre;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, sphereRadius, 1))
            finalPosition = hit.position;

        return finalPosition;
    }

    // Fires the equipped weapon. NOTE: Only called by AI controlled character
    private void FireWeapon()
    {
        // Reset fire delay timer
        if (m_delayTimer >= m_fireDelay)
            m_delayTimer = 0.0f;
        else
            return;
        
        // Fire equipped weapon
        for (int i = 0; i < m_inventory.slots.Length; i++)
        {
            if (m_inventory.isActive[i])
            {
                int weaponID = m_inventory.slots[i].gameObject.transform.GetComponentInChildren<ItemId>().itemId;

                float fireRate = m_weaponDatabase.weapons[weaponID].fireRate;
                m_fireDelay = UnityEngine.Random.Range(fireRate, fireRate * m_fireRateMultiplier);

                switch (weaponID)
                {
                    case 0:
                        AK_Script ak = GetComponentInChildren<AK_Script>();
                        ak.Fire();
                        m_isReloading = ak.GetIsReloading();
                        break;
                    case 1:
                        ShotgunScript shotgun = GetComponentInChildren<ShotgunScript>();
                        shotgun.Fire();
                        m_isReloading = shotgun.GetIsReloading();
                        break;
                    case 2:
                        RPG_Script rpg = GetComponentInChildren<RPG_Script>();
                        rpg.Fire();
                        break;
                    case 3:
                        M4_Script m4 = GetComponentInChildren<M4_Script>();
                        m4.Fire();
                        m_isReloading = m4.GetIsReloading();
                        break;
                    case 4:
                        HeavyRifleScript heavy = GetComponentInChildren<HeavyRifleScript>();
                        heavy.Fire();
                        m_isReloading = heavy.GetIsReloading();
                        break;
                    case 5:
                        SciFiPistolScript pistol = GetComponentInChildren<SciFiPistolScript>();
                        pistol.Fire();
                        m_isReloading = pistol.GetIsReloading();
                        break;
                    case 6:
                        SciFiRifleScript rifle = GetComponentInChildren<SciFiRifleScript>();
                        rifle.Fire();
                        m_isReloading = rifle.GetIsReloading();
                        break;
                    default:
                        break;
                }
            }
        }
    }

    // Returns true if the player is reloading
    public bool IsReloading()
    {
        return m_isReloading;
    }
    
    // Setters
    public void SetMovementSpeed(float speed)
    {
        m_playerController.SetMovementSpeed(speed);
        m_nav.speed = speed;
    }

    // Increments
    public void IncrementHealth(float health)
    {
        if (!m_isDead) m_health += health;
        if (m_health > m_maxHealth) m_health = m_maxHealth;
    }

    public void IncrementMaxHealth(float maxHealth) => m_maxHealth += maxHealth;

    public void IncrementMovementSpeed(float speed)
    {
        float movementSpeed = m_playerController.GetMovementSpeed();
        movementSpeed += speed;
        m_playerController.SetMovementSpeed(movementSpeed);
        m_nav.speed = movementSpeed;
    }

    // Decrements
    public void DecrementHealth(float health)
    {
        if (!m_isDead)
        {
            m_health -= health;
            if (m_health < 0.0f) m_health = 0.0f;
        }
    }

    public void DecrementMaxHealth(float maxHealth)
    {
        m_maxHealth -= maxHealth;
        if (m_maxHealth < 0.0f) m_maxHealth = 0.0f;
    }
    
    public void DecrementMovementSpeed(float speed)
    {
        float movementSpeed = m_playerController.GetMovementSpeed();
        movementSpeed -= speed;
        if (movementSpeed < 0.0f) movementSpeed = 0.0f;
        m_playerController.SetMovementSpeed(movementSpeed);
        m_nav.speed = movementSpeed;
    }

    // Returns true if the nav agent has reached its destination
    private bool IsNavAgentAtDest()
    {
        if (!m_nav.enabled || !m_nav.isOnNavMesh) return false;

        if (!m_nav.pathPending)
        {
            if (m_nav.remainingDistance <= m_nav.stoppingDistance)
            {
                if (!m_nav.hasPath || m_nav.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
        }

        return false;
    }

    /**
     * @brief Reads through a list of possible positions and returns 
     *        the closest position.
     * @param possiblePositions - The list of all possible positions.
     * @returns The closest position.
     */
    public Transform GetClosest(Transform[] possiblePositions) {

        Vector2 playerPosition_2D = new Vector2(gameObject.transform.position.x, gameObject.transform.position.z);

        Vector2 firstPosition_2D = new Vector2(possiblePositions[0].transform.position.x, possiblePositions[0].transform.position.z);

        float closestDistance = Vector2.Distance(playerPosition_2D, firstPosition_2D);

        Transform closestPosition = possiblePositions[0];

        for (int i = 1; i < possiblePositions.Length; i++) {
            Vector2 position_2D = new Vector2(possiblePositions[i].transform.position.x, possiblePositions[i].transform.position.z);

            float distance = Vector2.Distance(playerPosition_2D, position_2D);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPosition = possiblePositions[i];
            }
        }     

        return closestPosition;
    }
}
