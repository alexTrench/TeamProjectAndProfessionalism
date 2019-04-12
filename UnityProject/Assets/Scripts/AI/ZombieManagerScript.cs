using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieManagerScript : MonoBehaviour
{
    private List<Zombie> m_zombieCharacters;
    [SerializeField] private Transform[] spawnPoints = null;
    [SerializeField] private GameObject enemy_template = null;

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
     */
    public void Spawn() {
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        GameObject zombie = Instantiate(
            enemy_template, 
            spawnPoints[spawnIndex].position, 
            spawnPoints[spawnIndex].rotation,
            gameObject.transform
        ) as GameObject;

        zombie.SetActive(true);
        m_zombieCharacters.Add(zombie.GetComponent<Zombie>());
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
            if (m_zombieCharacters[i].IsDead()) {
                zombiesToRemove.Add(i);
                zombiesKilled++;
                GameplayManager.GM.EnemyHasDied();
            }
        }
        // Remove dead zombies from list
        foreach (var index in zombiesToRemove)
        {
            m_zombieCharacters.RemoveAt(index);
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
}
