using System.Collections.Generic;
using UnityEngine;

public class ZombieManagerScript : MonoBehaviour
{
    private List<Zombie> m_zombieCharacters;
    [SerializeField] private Transform[] spawnPoints = null;
    [SerializeField] private GameObject zombie_template_war = null;
    [SerializeField] private GameObject zombie_template_derrick = null;
    [SerializeField] private GameObject zombie_template_girl = null;

    //[viewTracker] For tracking where to spawn zombies.
    [SerializeField] private ViewTracker viewTracker = null;

    //[zombiesKilled] Tracks how many zombies have been killed
    //during the gameplay session.
    private int zombiesKilled = 0;

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
            switch(Random.Range(0, 3)) {
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

                List<Transform> possibleSpawnPoints = viewTracker.GetPointsNotInRange(spawnPoints);

                if(possibleSpawnPoints.Count > 0) {
                    //[spawnPoint] Where to spawn the zombie.
                    int spawnPoint = Random.Range(0, possibleSpawnPoints.Count);

                    //[zombie] The zombie being created.
                    GameObject zombie = Instantiate(
                        zombieType,
                        possibleSpawnPoints[spawnPoint].position,
                        possibleSpawnPoints[spawnPoint].rotation,
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
        // List to hold indexes of all zombies to remove from list
        List<int> zombiesToRemove = new List<int>();

        // Loop through zombies
        // If zombie is dead, add index to list so the zombie can be removed
        for (int i = 0; i < m_zombieCharacters.Count; i++)
        {
            if (m_zombieCharacters[i].IsDead())
            {
                zombiesToRemove.Add(i);
                OnZombieDeath();
            }
        }
        // Remove dead zombies from list
        foreach (var index in zombiesToRemove)
        {
            m_zombieCharacters.RemoveAt(index);
        }
    }

    /**
     * @brief Actions to occur when a zombie dies.
     */
    private void OnZombieDeath()
    {
        zombiesKilled++;
        GameplayManager.GM.EnemyHasDied();
        //Give the user EX (@Haoming)
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
