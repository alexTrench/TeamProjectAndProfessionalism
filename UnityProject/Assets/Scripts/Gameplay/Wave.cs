using UnityEngine;
/**
 * @brief   Manages a wave.
 * @extends MonoBehaviour
 * @author  Andrew Alford
 * @date    10/04/19
 * @version 1.0 - 10/04/19
 */
public class Wave
{
    //[totalWaves] How many waves have occurred so far.
    private static int totalWaves = 0;

    //[BASE_ENEMIES] A base used to calculate how many 
    //enemies to spawn in the wave.
    private const int BASE_ENEMIES = 10;

    //[DIMINISH_RETURNS] diminishes the returns 
    //calculated from base variables.
    private const float DIMINISH_RETURNS = 2.5f;

    //[numEnemies] How many enemies are currently on the map
    private int numEnemies;

    //[waveNo] The wave that is currently in session.
    private int waveNo;

    /**
     * @brief Constructor for a Wave.
     */
    public Wave() {
        //Update the wave number.
        waveNo = ++totalWaves;

        //Calculate the number of enemies to spawn in this wave.
        numEnemies = (int)(waveNo * (BASE_ENEMIES / DIMINISH_RETURNS));

        Debug.Log("num enemies:\t" + numEnemies);
    }
}
