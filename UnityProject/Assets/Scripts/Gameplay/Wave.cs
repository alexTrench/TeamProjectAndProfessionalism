using System.Collections;
using UnityEngine;

/**
 * @brief   Manages a wave.
 * @author  Andrew Alford
 * @date    10/04/19
 * @version 1.0 - 10/04/19
 */
public class Wave
{
    //[totalWaves] How many waves have occurred so far.
    public static int WAVE_ID = 0;

    //[BASE_ENEMIES] A base used to calculate how many 
    //enemies to spawn in the wave.
    private const int BASE_ENEMIES = 10;

    //[DIMINISH_RETURNS] diminishes the returns 
    //calculated from base variables.
    private const float DIMINISH_RETURNS = 2.5f;

    //[MAX_ENEMIES] The maxium number of 
    //enemies that can exist in the game.
    private const int MAX_ENEMIES = 50;

    private const float SPAWN_INTERVAL = 1.0f;

    //[totalEnemies] How many enemies will exist in this wave.
    private int totalEnemies = 0;

    //[enemiesRemaining] Tracks the current number of enemies in the wave.
    private int enemiesRemaining = 0;

    //[waveNo] The wave that is currently in session.
    private readonly int waveNo;

    /**
     * @brief Constructor for a Wave.
     */
    public Wave(ZombieManagerScript zombieManager) {
        //Update the wave number.
        waveNo = ++WAVE_ID;

        //Calculate the number of enemies to spawn in this wave.
        totalEnemies = (int)(waveNo * (BASE_ENEMIES / DIMINISH_RETURNS));

        //Reset the number of enemies remaining.
        enemiesRemaining = totalEnemies;
        
        //Spawn all the zombies in the wave.
        for(int i = 0; (i < totalEnemies) && (i < MAX_ENEMIES); i++) {
            zombieManager.Spawn();
        }
    }

    public void SetNumEnemies(int numEnemies) {
        enemiesRemaining = numEnemies;
    }

    //@reutrns the number of enemies left to fight in the wave.
    public int GetNumEnemies() => enemiesRemaining;
}
