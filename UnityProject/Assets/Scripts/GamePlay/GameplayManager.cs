using System.Collections;
using UnityEngine;

/**
 * @brief   Manages the games gameplay.
 * @extends MonoBehaviour
 * @author  Andrew Alford
 * @date    10/04/19
 * @version 1.1 - 11/04/19
 */
public class GameplayManager : MonoBehaviour
{
    //[GM] The singleton of this object.
    public static GameplayManager GM;

    //[enemyManager] Manages all the enemies in the game.
    [SerializeField] ZombieManagerScript enemyManager = null;

    //[characterManager] Manages all of the characters in the game.
    [SerializeField] CharacterManagerScript characterManager = null;

    //[COOLDOWN] The amount of time (in seconds) 
    //allocated between waves.
    public const float COOLDOWN = 15.0f;

    //[currentWave] The wave currently in progress.
    private Wave currentWave = null;

    //[cooldownInProgress] 'true' inbetween waves.
    private bool cooldownInProgress = false;

    /**
     * @brief runs immediatly to make sure there 
     *        is only one instance of this class.
     */
    void Awake() {
        //DECLARE AS SINGLETON...

        //Code to make the Game Manager a singleton.
        if (!GM) {
            //If the game manager does not yet exist,
            //assign it to this instance.
            DontDestroyOnLoad(gameObject);
            GM = this;
        }
        else if (GM != this) {
            Destroy(gameObject);
        }
    }

    /**
     * @brief Updates the Gameplay Manager once every frame.
     */
    private void Update() {
        //Start a new wave.
        if(!WaveInProgress() && !cooldownInProgress) {
            StartWave();
        }
        //Wave in session.
        else if(WaveInProgress() && !cooldownInProgress) {
            //Check to end the current wave.
            if(currentWave.GetNumEnemiesRemaining() == 0) {
                EndWave();
            }
        }
        //Cooldown in session.
        else if(!WaveInProgress() && cooldownInProgress) { }
        //Error
        else if(WaveInProgress() && cooldownInProgress) {
            Debug.LogError("Unrecognised gameplay state");
        }
    }

    /**
     * @brief Begins the next wave.
     */
    private void StartWave() {
        currentWave = new Wave(5);
        StartCoroutine(currentWave.SpawnEnemies(enemyManager));
    }

    /**
     * @brief Ends the current wave.
     */
    private void EndWave() {
        currentWave = null;
        //Begin the cooldown period.
        StartCoroutine(CooldownPeriod());
    }

    /**
     * @brief Waits until the cooldown period is over.
     */
    private IEnumerator CooldownPeriod() {
        cooldownInProgress = true;
        //Repawn the most the player who most recently died.
        characterManager.RevivePlayer();
        yield return new WaitForSeconds(COOLDOWN);
        cooldownInProgress = false;
    }

    //@reutrns the current wave in progress.
    public Wave GetWave() => currentWave;

    //@returns 'true' if a wave is currently in progress.
    public bool WaveInProgress() => !(currentWave == null);

    //@returns 'true' if the cooldown period is in progress.
    public bool IsCooldownInProgres() => cooldownInProgress;

    /**
     * @brief Call this method whenever an enemy 
     *        has died to decrement the number 
     *        of enemies remaining in the wave.
     */
    public void EnemyHasDied() {
        if (WaveInProgress()) {
            currentWave.DecrementEnemiesRemaining();
        }
    }

    /**
     * @brief End the game.
     */
    public void EndGame() {
        Wave.Reset();
        GM = null;
    }
}
