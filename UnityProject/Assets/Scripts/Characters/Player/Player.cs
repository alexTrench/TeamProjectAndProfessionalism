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
    private weaponDatabase         m_weaponDatabase;
    private bool                   m_isReloading        = false;

    [SerializeField] private readonly float m_distanceThreshold  = 5.0f;
    [SerializeField] private readonly float m_navMeshRadius      = 3.0f;
    [SerializeField] private readonly float m_targetCircleRadius = 0.1f;
    [SerializeField] private readonly float m_fireRateMultiplier = 2.5f;

    private float m_fireDelay  = 1.0f;
    private float m_delayTimer = 0.0f;

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
        Debug.Assert(m_inventory != null, "Could not find Inventory!");

        // Get WeaponDatabase script
        m_weaponDatabase = GameObject.FindGameObjectWithTag("GameController").GetComponent<weaponDatabase>();
        Debug.Assert(m_weaponDatabase != null, "Could not find WeaponDatabase!");

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
                m_nav.SetDestination(GetRandomNavMeshLocation(m_navMeshRadius, m_characterManager.GetCurrentPlayer().transform.position));

            // Look at nearest zombie target
            if (m_zombieManager.GetNumOfZombies() > 0)
                m_lookAtTransform = GetNearestZombie().transform;
            transform.LookAt(m_lookAtTransform);

            // Shoot at nearest target
            if (m_zombieManager.GetNumOfZombies() > 0)
                FireWeapon();
        }
        else
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
                        default:
                            break;
                    }
                }
            }
        }

        // Update delay timer
        m_delayTimer += Time.deltaTime;
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

    // Returns a random position within a sphere of the given radius and centre point
    private Vector3 GetRandomNavMeshLocation(float sphereRadius, in Vector3 sphereCentre)
    {
        Vector3 randomDirection = Random.insideUnitSphere * sphereRadius;
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

        // Get target position
        Vector3 targetPosition = GetRandomNavMeshLocation(m_targetCircleRadius, m_lookAtTransform.position);
        //GameObject obj = new GameObject();
        //obj.transform.SetPositionAndRotation( m_lookAtTransform.position, m_lookAtTransform.rotation);
        //obj.transform.position = targetPosition - m_lookAtTransform.position;

        //m_lookAtTransform = obj.transform;
        //transform.LookAt(m_lookAtTransform);

        // Fire equipped weapon
        for (int i = 0; i < m_inventory.slots.Length; i++)
        {
            if (m_inventory.isActive[i])
            {
                int weaponID = m_inventory.slots[i].gameObject.transform.GetComponentInChildren<ItemId>().itemId;

                float fireRate = m_weaponDatabase.weapons[weaponID].fireRate;
                m_fireDelay = Random.Range(fireRate, fireRate * m_fireRateMultiplier);

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
}
