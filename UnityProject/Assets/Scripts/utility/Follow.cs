using UnityEngine;

/**
 * @brief Helper class to allow one object to follow another.
 * @extends MonoBehaviour
 * @author Andrew Alford
 * @date 25/03/2019
 * @version 1.0 - 25/03/2019
 */
public class Follow : MonoBehaviour
{
    //[target] What the camera should be following.
    [SerializeField] private Transform target = null;
    //[offset] The offset between the camera and the offset.
    [SerializeField] private Vector3 offset = new Vector3(0, 0, 0);

    /**
     * @brief Updates the Follower's position.
     */
    private void LateUpdate() {
        transform.position = target.position + offset;
    }
}
