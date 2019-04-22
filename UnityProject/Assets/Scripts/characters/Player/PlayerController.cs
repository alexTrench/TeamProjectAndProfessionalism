using UnityEngine;

/**
 * @brief   Controls a character with user input.
 * @extends MonoBehaviour
 * @date    25/03/2019
 * @version 1.1 - 26/03/2019
 */
public class PlayerController : MonoBehaviour {
    //[movementSpeed] How fast the player is moving.
    [SerializeField] private float movementSpeed = 10.0f;

    //@brief Updates the player.
    private void FixedUpdate() {
        if(!OpenPauseMenu.IsPaused()) {

            Vector3 vertical = new Vector3();
            Vector3 horizontal = new Vector3();

            if (InputManager.Forward()) {
                vertical = transform.forward * movementSpeed * Time.deltaTime;
            } else if (InputManager.Backward()) {
                vertical = -transform.forward * movementSpeed * Time.deltaTime;
            }

            if (InputManager.Right()) {
                horizontal = transform.right * movementSpeed * Time.deltaTime;
            } else if (InputManager.Left()) {
                horizontal = -transform.right * movementSpeed * Time.deltaTime;
            }

            transform.position += vertical + horizontal;

            //Rotate the player.
            InputManager.LookAtAxis(transform);

        }
    }

    //Retrives the players movement speed.
    public float GetMovementSpeed() => movementSpeed;

    /**
     * @brief Allows the player's movement speed to be set.
     * @param newMovementSpeed - The new movement speed
     *                           for the player.
     */
    public void SetMovementSpeed(float newMovementSpeed) => movementSpeed = newMovementSpeed;
}