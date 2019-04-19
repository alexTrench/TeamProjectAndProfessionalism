using UnityEngine;

/**
 * @brief   Controls a character with user input.
 * @extends MonoBehaviour
 * @date    25/03/2019
 * @version 1.1 - 26/03/2019
 */
public class PlayerController : MonoBehaviour {
    //[movementSpeed] How quickly the player is moving.
    [SerializeField] private float movementSpeed = 12.0f;
    
    /**
     * @brief Initialises the Player Controller.
     */
    private void Awake() {
        Cursor.visible = false;
    }

    /**
     * Updates the player once every frame.
     */
    void Update()
    {
        if(!OpenPauseMenu.IsPaused()) {
            Vector3 transformModifier  = new Vector3(0, 0, 0);

            //Move the player.
            if(InputManager.Forward()) {
                transform.position += transform.forward * movementSpeed * Time.deltaTime;
            }
            if(InputManager.Backward()) {
                transform.position += -transform.forward * movementSpeed * Time.deltaTime;
            }
            if(InputManager.Right()) {
                transform.position += transform.right * movementSpeed * Time.deltaTime;
            }
            if(InputManager.Left()) {
                transform.position += -transform.right * movementSpeed * Time.deltaTime;
            }

            //Rotate the player.
            InputManager.LookAtAxis(transform);
        }
    }

    public float GetMovementSpeed()
    {
        return movementSpeed;
    }

    public void SetMovementSpeed(float newMovementSpeed)
    {
        movementSpeed = newMovementSpeed;
    }
}