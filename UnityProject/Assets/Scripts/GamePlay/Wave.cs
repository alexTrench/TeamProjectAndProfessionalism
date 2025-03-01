﻿using System;
using System.Collections;
using UnityEngine;

/**
 * @brief   Manages a wave.
 * @author  Andrew Alford
 * @date    10/04/19
 * @version 1.2 - 19/04/19
 */
public class Wave
{
    //[totalWaves] How many waves have occurred so far.
    private static int WAVE_ID = 0;

    //[waveNo] The wave that is currently in session.
    private readonly int waveNo;

    //[spawningPeriodInProgress] While 'true' the 
    //spawning period is currently in progress.
    private bool spawningPeriodInProgress = false;

    //[DIMINISH_RETURNS] diminishes the returns 
    //calculated from base variables.
    private const float DIMINISH_RETURNS = 2.5f;

    //[MIN_SPAWN_INTERVAL] The minimum spawn interval allowed.
    private const float MIN_SPAWN_INTERVAL = 0.01f;

    //[spawnInterval] How frequently to spawn enemies.
    private float spawnInterval = 12f;

    //[BASE_ENEMIES] A base used to calculate how many 
    //enemies to spawn in the wave.
    private const int BASE_ENEMIES = 16;

    //[MAX_ENEMIES] The maxium number of 
    //enemies that can exist in the game.
    private const int MAX_ENEMIES = 250;

    //[totalEnemies] How many enemies will exist in this wave.
    private int totalEnemies = 0;

    //[enemiesRemaining] Tracks the current number of enemies in the wave.
    private int enemiesRemaining = 0;

    /**
     * @brief Constructor for a Wave.
     */
    public Wave(int jump = 0) {
        waveNo = jump + ++WAVE_ID;        

        CalculateNumEnemies();

        //Reset the number of enemies remaining.
        enemiesRemaining = totalEnemies;
    }

    /**
     * @brief Calculates how many enemies will be 
     *        spawned in the wave.
     */
    private void CalculateNumEnemies() {
        Debug.Log("Wave num: " + waveNo);
        //Calculate the number of enemies to spawn in this wave.
        totalEnemies = (int)(waveNo * (BASE_ENEMIES / DIMINISH_RETURNS));
        //Clamp the number of enemies to help with the games performance.
        if (totalEnemies > MAX_ENEMIES) {
            totalEnemies = MAX_ENEMIES;
        }
    }

    /**
     * @brief Coroutine to spawn enemies over a set period of time.
     * @param enemyManager - Manages all the enemies in the wave.
     */
    public IEnumerator SpawnEnemies(ZombieManagerScript enemyManager) {
        
        spawningPeriodInProgress = true;
        CalculateSpawnInterval();

        Debug.Log("Wave: " + GetWaveID() + "\tEnemies: " + totalEnemies);

        float waveModifer = (waveNo / (DIMINISH_RETURNS * 5));
        if(waveModifer > 5.0f) { waveModifer = 5.0f; }
        Debug.Log("modifer:\t" + waveModifer);

        for (int i = 0; (i < totalEnemies) && (i < MAX_ENEMIES); i++) {
            if(enemyManager != null && enemyManager.isActiveAndEnabled) {
                enemyManager.Spawn(waveModifer);
                yield return new WaitForSeconds(spawnInterval);
            }
        }
        spawningPeriodInProgress = false;
    }

    /**
     * @brief Calculates how frequently enemies spawn.
     */
    private void CalculateSpawnInterval() {
        //Calculate the interval to 2dp.
        spawnInterval = (float)Math.Round(
            (spawnInterval /= waveNo * DIMINISH_RETURNS) * 100f) / 100f;

        //Clamp the interval if neccassery.
        if (spawnInterval < MIN_SPAWN_INTERVAL) {
            spawnInterval = MIN_SPAWN_INTERVAL;
        }
    }

    //@returns the total number of waves that have occurred so far.
    public static int GetWaveID() => WAVE_ID;

    //@brief Decrements the number of enemies remaining in the wave.
    public void DecrementEnemiesRemaining() => enemiesRemaining--;

    //@reutrns the number of enemies left to fight in the wave.
    public int GetNumEnemiesRemaining() => enemiesRemaining;

    //@returns the total number of enemies that will be in the wave.
    public int GetNumEnemiesTotal() => totalEnemies;

    //@returns 'true' if the spawning period is currently in progress.
    public bool IsSpawningPeriodInProgress() => spawningPeriodInProgress;

    //@brief Resets the wave ID.
    public static void Reset() => WAVE_ID = 0;
}
