using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckEnemies : MonoBehaviour
{
    // variable which is accesible from inside the editor in order to link scene objects
    [SerializeField] private Text enemiesLeftText = null; // the UI Text element for enemies left

    
    private int enemiesLeft;        // local variable that counts the enemies left
    private int updatedEnemiesLeft; // local variable that counts the updated number of enemies left

    // local zombie manager
    private ZombieManagerScript zombieManager;

    // Start is called before the first frame update
    private void Start()
    {
        // look up in the list of objects and get the 'ZombieManagerScript' component for ZombieManager
        zombieManager = GameObject.FindGameObjectWithTag("ZombieManager").GetComponent<ZombieManagerScript>();

        // initialise the number of zombies and update it inside the GUI text
        enemiesLeft          = zombieManager.GetNumOfZombies();
        enemiesLeftText.text = enemiesLeft.ToString();

    }

    // Update is called once per frame
    private void Update()
    {
        if(GameplayManager.GM != null)
        {
            if (!(GameplayManager.GM.GetWave() == null))
            {
                // update the number of zombies each frame
                updatedEnemiesLeft = GameplayManager.GM.GetWave().GetNumEnemiesRemaining();

                // if the number of zombies has changes
                if (updatedEnemiesLeft != enemiesLeft)
                {
                    // update the GUI text
                    enemiesLeft = GameplayManager.GM.GetWave().GetNumEnemiesRemaining();
                    enemiesLeftText.text = updatedEnemiesLeft.ToString();
                }
            }
        }
    }
}
