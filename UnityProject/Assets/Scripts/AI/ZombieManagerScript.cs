using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieManagerScript : MonoBehaviour
{
    private List<Zombie> m_zombieCharacters;

    private void Awake()
    {
        // Add zombies already in scene to list
        m_zombieCharacters = new List<Zombie>();
        GameObject[] zombies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var zombie in zombies)
        {
            m_zombieCharacters.Add(zombie.GetComponent<Zombie>());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Empty
    }

    // Update is called once per frame
    void Update()
    {
        // List to hold indexes of all zombies to remove from list
        List<int> zombiesToRemove = new List<int>();

        // Loop through zombies
        // If zombie is dead, add index to list so the zombie can be removeed
        for (int i = 0; i < m_zombieCharacters.Count; i++)
        {
            if (m_zombieCharacters[i].IsDead())
                zombiesToRemove.Add(i);
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
