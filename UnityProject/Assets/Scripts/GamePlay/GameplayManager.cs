using System.Collections;
using UnityEngine;

/**
 * @brief   Manages the games gameplay.
 * @extends MonoBehaviour
 * @author  Andrew Alford
 * @date    10/04/19
 * @version 1.0 - 10/04/19
 */
public class GameplayManager : MonoBehaviour
{
    public static GameplayManager GM;

    //[COOLDOWN] The amount of time (in seconds) 
    //allocated between waves.
    private const float COOLDOWN = 10.0f;

    //[currentWave] The wave currently in progress.
    private Wave currentWave = null;

    //[cooldownInProgress] 'true' inbetween waves.
    public bool cooldownInProgress = false;

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

    private void Update() {
        //Start a new wave.
        if(!WaveInProgress() && !cooldownInProgress) {
            StartCoroutine(StartWave());
        }
        //Wave in session.
        else if(WaveInProgress() && !cooldownInProgress) {
            if(currentWave.GetNumEnemies() == 0) {
                StartCoroutine(EndWave());
            }
        }
        //Cooldown in session.
        else if(!WaveInProgress() && cooldownInProgress) {

        }
        //Error
        else {
            Debug.LogError("Unrecognised gameplay state");
        }
    }

    private IEnumerator StartWave() {
        yield return currentWave = new Wave();
        Debug.Log("Wave start");
    }

    private IEnumerator EndWave() {
        yield return currentWave = null;
        Debug.Log("Wave end");
        //Begin the cooldown period.
        StartCoroutine(CooldownPeriod());
    }

    private IEnumerator CooldownPeriod() {
        cooldownInProgress = true;
        yield return new WaitForSeconds(COOLDOWN);
        cooldownInProgress = false;
    }

    //@reutrns the current wave in progress.
    public Wave GetWave() => currentWave;

    //@returns 'true' if a wave is currently in progress.
    public bool WaveInProgress() => !(currentWave == null);

    public bool IsCooldownInProgres() => cooldownInProgress;
}
