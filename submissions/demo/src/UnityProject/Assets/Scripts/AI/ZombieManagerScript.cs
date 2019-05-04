using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieManagerScript : MonoBehaviour
{
    private List<Zombie> m_zombieCharacters;
    [SerializeField] private Transform[] spawnPoints = null;
    [SerializeField] private GameObject zombie_template_war = null;
    [SerializeField] private GameObject zombie_template_derrick = null;
    [SerializeField] private GameObject zombie_template_girl = null;
    [SerializeField] private GameObject[] powerUps = null;
    [SerializeField] private GameObject canvas = null;
    private Playerstats stats;



    //[viewTracker] For tracking where to spawn zombies.
    [SerializeField] private ViewTracker viewTracker = null;
    //[characterManager] Tracks the position of the player
    //to spawn zombies nearby
    [SerializeField] private CharacterManagerScript characterManager = null;

    //[zombiesKilled] Tracks how many zombies have been killed
    //during the gameplay session.
    private int zombiesKilled = 0;

    private void Start()
    {
        stats = canvas.GetComponent<Playerstats>();
    }

    private void Awake()
    {
        // Add zombies already in scene to list
        m_zombieCharacters = new List<Zombie>();
    }

    /**
     * @brief Spawns an enemy in a random location.
     * @param waveModifier - Modifiers to affect the enemies stats.
     */
    public void Spawn(float waveModifier) {
        if(gameObject != null && gameObject.activeInHierarchy) {
            
            //[zombieType] The type of zombie to be created.
            GameObject zombieType = null;

            string zomType = ""; //temp for debugging.

            //Work out which type of zombie to create.
            switch(UnityEngine.Random.Range(0, 3)) {
                case (0):
                    zomType = "War Zombie";
                    zombieType = zombie_template_war;
                    break;
                case (1):
                    zomType = "Zombie Girl";
                    zombieType = zombie_template_girl;
                    break;
                case (2):
                    zomType = "Zombie Derrick";
                    zombieType = zombie_template_derrick;
                    break;
            }

            //Spawn the zombie.
            if(zombieType != null) {

                //[spawnPoint] Holds the spawn point closest to the player 
                //that is not viewable by the camera.
                Transform spawnPoint = characterManager.GetCurrentPlayer().GetClosest(
                    viewTracker.GetPointsNotInRange(spawnPoints).ToArray()
                );

                if(spawnPoint != null) {

                    //[zombie] The zombie being created.
                    GameObject zombie = Instantiate(
                        zombieType,
                        spawnPoint.position,
                        spawnPoint.rotation,
                        gameObject.transform
                    ) as GameObject;

                    zombie.SetActive(true);

                    zombie.GetComponent<ZombieAttack>().
                        ApplyAttackDamageModifier(waveModifier);
                    zombie.GetComponent<ZombieAnimationScript>().
                        ApplySpeedModifier(waveModifier);

                    m_zombieCharacters.Add(zombie.GetComponent<Zombie>());
                }
                else {
                    Debug.LogError("Nowhere to spawn zombie");
                }

            } else {
                Debug.LogError("Unable to spawn:\t"+zomType);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        // Loop through zombies and check for dead ones 
        for (int i = 0; i < m_zombieCharacters.Count; i++)
        {
            Zombie zombie = m_zombieCharacters[i];

            if (zombie.IsDead())
            {
                DropPowerUp(zombie.transform);
                OnZombieDeath();
            }
        }

        // Remove all dead zombies from list
        m_zombieCharacters.RemoveAll(zombie => zombie.IsDead() == true);
    }

    /**
     * @brief Actions to occur when a zombie dies.
     */
    private void OnZombieDeath()
    {
        zombiesKilled++;
        GameplayManager.GM.EnemyHasDied();
        //Give the user EX (@Haoming)
        stats.Addexp();
    }


    private void DropPowerUp(Transform transfor)
    {
        int randomNumber = UnityEngine.Random.Range(0, 100);

        if(randomNumber == 1)
        {
            Instantiate(powerUps[0], transfor.position, powerUps[0].gameObject.transform.rotation);
            Debug.Log("drop");
        }
        if (randomNumber == 3)
        {
            Instantiate(powerUps[1], transfor.position, powerUps[1].gameObject.transform.rotation);
            Debug.Log("drop");

        }
        if (randomNumber == 5)
        {
            Instantiate(powerUps[2], transfor.position, powerUps[2].gameObject.transform.rotation);
            Debug.Log("drop");
        }
        if (randomNumber == 7)
        {
            Instantiate(powerUps[3], transfor.position, powerUps[3].gameObject.transform.rotation);
            Debug.Log("drop");
        }
        if (randomNumber == 9)
        {
            Instantiate(powerUps[4], transfor.position, powerUps[4].gameObject.transform.rotation);
            Debug.Log("drop");
        }

    }

    // Returns a list of all zombie characters
    public List<Zombie> GetZombieCharacters()
    {
        return m_zombieCharacters;
    }

    // Adds the given zombie character to list of all zombies
    public void AddZombieCharacter(Zombie zombie)
    {
        m_zombieCharacters.Add(zombie);
    }

    // Returns the number of zombie characters in the list
    public int GetNumOfZombies()
    {
        return m_zombieCharacters.Count;
    }

    //@returns the total number of zombies the player has killed.
    public int GetNumberOfZombiesKilled() => zombiesKilled;
}
