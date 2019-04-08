using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownScript : MonoBehaviour
{
    [SerializeField] private Text  uiNumber;
    [SerializeField] private float mainTimer;
    [SerializeField] private Text  enemiesLeft;

    private float timer;
    private int enemiesLeftINT;

    private void Start()
    {
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
                enemiesLeft.text = "10";
                timer = mainTimer;
            } else
            {
                uiNumber.text = "NOW";
            }
        }
    }
}
