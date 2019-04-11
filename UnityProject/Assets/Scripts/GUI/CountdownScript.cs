using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownScript : MonoBehaviour
{
    // variables which are accesible from inside the editor in order to link scene objects
    [SerializeField] private Text uiNumber    = null;  // UI Text element for the next wave counter
    [SerializeField] private Text enemiesLeft = null;  // UI Text element for the number of enemies left

    // variable that contains the main timer of the game
    private float mainTimer = GameplayManager.COOLDOWN;

    // local zombie manager
    private ZombieManagerScript zombieManager;

    // local integers that hold the number of enemies
    private int enemiesNo;
    private int enemiesLeftINT;

    // local float timer
    private float timer;
    

    private void Start()
    {
        // look on the list of objects and get the component for character manager script
        zombieManager = GameObject.FindGameObjectWithTag("ZombieManager").GetComponent<ZombieManagerScript>();

        // initialise the timer
        timer = mainTimer;
    }

    private void Update()
    {
        // update the counter for enemies left
        enemiesLeftINT = int.Parse(enemiesLeft.text);

        // if the timer is initialised and all the enemies have been neutralised
        if (timer >= 0.0f && enemiesLeftINT <= 0)
        {
            // start the countdown and update it inside the UI Text element
            timer -= Time.deltaTime;
            uiNumber.text = timer.ToString("F");
        }
        else
        {
            // otherwise, if the number of enemies is lower than 0
            if(enemiesLeftINT <= 0)
            {
                // get the actual number of enemies from inside zombieManager
                // and update it inside the UI Text element
                enemiesNo = zombieManager.GetNumOfZombies();
                enemiesLeft.text = enemiesNo.ToString();

                // if the number of enemies is different than 0
                if(enemiesNo != 0)
                {
                    // reinitialise the timer
                    timer = mainTimer;
                } else
                {
                    // otherwise keep the UI Text element displaying 'NOW'
                    uiNumber.text = "NOW";
                }
            } else
            {
                // otherwise keep the UI Text element displaying 'NOW'
                uiNumber.text = "NOW";
            }
        }
    }
}
