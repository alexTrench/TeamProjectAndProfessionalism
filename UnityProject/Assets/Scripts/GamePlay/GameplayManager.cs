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
    private const float COOLDOWN = 30.0f;

    //[currentWave] The wave currently in progress.
    private Wave currentWave = null;

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
     * @brief Starts up the Gameplay Manager.
     */
    private void Start() {
        StartWave();
    }

    private void StartWave() {
        currentWave = new Wave();
    }

    private void EndWave() {

    }

    //@reutrns the current wave in progress.
    public Wave GetWave() => currentWave;


}
