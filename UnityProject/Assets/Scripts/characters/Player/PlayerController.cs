using UnityEngine;

/**
 * @brief   Controls a character with user input.
 * @extends MonoBehaviour
 * @date    25/03/2019
 * @version 1.1 - 26/03/2019
 */
public class PlayerController : MonoBehaviour {
    //[movementSpeed] How quickly the player is moving.
    [SerializeField] private int movementSpeed = 12;


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

            if(InputManager.usingXboxOneController()) {
                //Rotate with controller
                Vector3 playerDirection = Vector3.right * 
                InputManager.lookRightAxis.ToFloat() + 
                Vector3.forward * -InputManager.lookForwardAxis.ToFloat();

                //If the player has moved.
                if (playerDirection.sqrMagnitude > 0.0f) {
                    transform.rotation = Quaternion.LookRotation(
                        playerDirection, 
                        Vector3.up
                    );
                }
            } else {
                Plane floor = new Plane(Vector3.up, new Vector3(0, this.transform.position.y, 0));

                //Rotate with mouse
                Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (floor.Raycast(cameraRay, out float rayLength))
                {
                    Vector3 pointToLook = cameraRay.GetPoint(rayLength);
                    Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);

                    transform.LookAt(new Vector3(
                        pointToLook.x,
                        transform.position.y,
                        pointToLook.z
                    ));
                }
            }
        }
    }
}