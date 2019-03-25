using UnityEngine;

/**
 * @brief Controls a character with user input.
 * @extends MonoBehaviour
 * @date 25/03/2019
 * @version 1.0 - 25/03/2019
 */
public class PlayerController : MonoBehaviour
{
    //[movementSpeed] How quickly the player is moving.
    [SerializeField] private int movementSpeed;

    /**
     * Updates the player once every frame.
     */
    void Update()
    {
        //Move the player.
        if(InputManager.Forward()) {
            transform.position += transform.forward / movementSpeed;
        }
        if(InputManager.Backward()) {
            transform.position += -transform.forward / movementSpeed;
        }
        if(InputManager.Right()) {
            transform.position += transform.right / movementSpeed;
        }
        if(InputManager.Left()) {
            transform.position += -transform.right / movementSpeed;
        }
    }
}
