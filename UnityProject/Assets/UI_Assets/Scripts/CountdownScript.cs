using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownScript : MonoBehaviour
{
    [SerializeField] private Text  uiNumber;
    [SerializeField] private float mainTimer;
    [SerializeField] private Text  enemiesLeft;

    private ZombieManagerScript zombieManager;
    private int enemiesNo;
    
    private float timer;
    private int enemiesLeftINT;

    private void Start()
    {
        // look on the list of objects and get the component for character manager script
        zombieManager = GameObject.FindGameObjectWithTag("ZombieManager").GetComponent<ZombieManagerScript>();

        timer = mainTimer;
    }

    private void Update()
    {
        enemiesLeftINT = int.Parse(enemiesLeft.text);

        if (timer >= 0.0f && enemiesLeftINT <= 0)
        {
            timer -= Time.deltaTime;
            uiNumber.text = timer.ToString("F");
        }
        else
        {
            if(enemiesLeftINT <= 0)
            {
                enemiesNo = zombieManager.GetNumOfZombies();
                enemiesLeft.text = enemiesNo.ToString();
                if(enemiesNo != 0)
                {
                    timer = mainTimer;
                } else
                {
                    uiNumber.text = "NOW";
                }
            } else
            {
                uiNumber.text = "NOW";
            }
        }
    }
}
