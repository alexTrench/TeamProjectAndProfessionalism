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
    private bool  canCount = true;
    private bool  doOnce = false;

    private void Start()
    {
        timer = mainTimer;
    }

    private void Update()
    {
        if(timer >= 0.0f && canCount)
        {
            timer -= Time.deltaTime;
            uiNumber.text = timer.ToString("F");
        } else if (timer <= 0.0f && !doOnce)
        {
            canCount = false;
            doOnce = true;
            uiNumber.text = "0.00";
            timer = 0.0f; 
        }
    }
}
