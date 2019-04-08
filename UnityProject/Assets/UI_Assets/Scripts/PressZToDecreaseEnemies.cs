using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PressZToDecreaseEnemies : MonoBehaviour
{
    [SerializeField] private Text enemiesLeft;

    private int enemiesLeftINT;

    // Start is called before the first frame update
    private void Start()
    {
        enemiesLeftINT = int.Parse(enemiesLeft.text);
        
    }

    // Update is called once per frame
    private void Update()
    {
        enemiesLeftINT = int.Parse(enemiesLeft.text);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if(enemiesLeftINT > 0)
            {
                --enemiesLeftINT;
                enemiesLeft.text = enemiesLeftINT.ToString();
            }
        }
    }
}
