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

    //[totalEnemies] How many enemies will exist in this wave.
    private int totalEnemies = 0;

    //[enemiesRemaining] Tracks the current number of enemies in the wave.
    private int enemiesRemaining = 0;

    //[waveNo] The wave that is currently in session.
    private readonly int waveNo;

    /**
     * @brief Constructor for a Wave.
     */
    public Wave() {
        //Update the wave number.
        waveNo = ++totalWaves;

        Debug.Log(waveNo);

        //Calculate the number of enemies to spawn in this wave.
        totalEnemies = (int)(waveNo * (BASE_ENEMIES / DIMINISH_RETURNS));

        //Reset the number of enemies remaining.
        enemiesRemaining = totalEnemies;

        Debug.Log("num enemies:\t" + totalEnemies);
    }

    //@reutrns the number of enemies left to fight in the wave.
    public int GetNumEnemies() => enemiesRemaining;
}
