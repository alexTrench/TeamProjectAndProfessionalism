using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckEnemies : MonoBehaviour
{
    [SerializeField] private Text enemiesLeftText;

    private int enemiesLeft;
    private int updatedEnemiesLeft;

    private ZombieManagerScript zombieManager;

    // Start is called before the first frame update
    private void Start()
    {
        // look on the list of objects and get the component for character manager script
        zombieManager = GameObject.FindGameObjectWithTag("ZombieManager").GetComponent<ZombieManagerScript>();

        enemiesLeft = zombieManager.GetNumOfZombies();
        enemiesLeftText.text = enemiesLeft.ToString();

    }

    // Update is called once per frame
    private void Update()
    {
        updatedEnemiesLeft = zombieManager.GetNumOfZombies();
        if(updatedEnemiesLeft != enemiesLeft)
        {
            enemiesLeftText.text = updatedEnemiesLeft.ToString();
            enemiesLeft = updatedEnemiesLeft;
        }
    }
}
