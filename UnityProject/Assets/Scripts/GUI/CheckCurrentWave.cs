using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckCurrentWave : MonoBehaviour
{
    // variable which is accesible from inside the editor in order to link scene objects
    [SerializeField] private Text waveText           = null; // the UI Text element for Wave Count Number
    [SerializeField] private Text endGameWaveText    = null; // the UI Text element for Wave Count Number
    [SerializeField] private Text endGameEnemiesText = null; // the UI Text element for Wave Count Number

    private int currentWave; // local variable that memorise the current wave
    private int updatedWave; // local variable that reads the updated wave

    // local zombie manager
    private ZombieManagerScript zombieManager;

    // Start is called before the first frame update
    void Start()
    {
        // look up in the list of objects and get the 'ZombieManagerScript' component for ZombieManager
        zombieManager = GameObject.FindGameObjectWithTag("ZombieManager").GetComponent<ZombieManagerScript>();

        // initialise the wave number and update the GUI Text Element
        currentWave = Wave.GetWaveID();
        waveText.text = currentWave.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        // update the wave number
        updatedWave = Wave.GetWaveID();

        // check whether or not the updated wave number has changed
        if(updatedWave != currentWave)
        {
            // update the GUI Text Element and the current wave variable
            waveText.text = updatedWave.ToString();
            currentWave = updatedWave;
        }

        // if it is end game
        if(OpenEndGameMenu.IsEndGame())
        {
            // update the number of waves survived inside End Game Panel
            endGameWaveText.text = updatedWave.ToString();
            endGameEnemiesText.text = zombieManager.GetNumberOfZombiesKilled().ToString();
        }
    }
}
