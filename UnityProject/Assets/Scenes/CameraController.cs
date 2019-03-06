using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject m_activePlayer;

    private Vector3 m_offset;

    // Start is called before the first frame update
    void Start()
    {
        // Make sure the camera is positioned behind the player (just in case 
        // the camera was altered in the editor)
        transform.position.Set(m_activePlayer.transform.position.x,
                               m_activePlayer.transform.position.y + 5.0f,
                               m_activePlayer.transform.position.z - 2.0f);
        transform.rotation.Set(0.0f, 85.0f, 0.0f, 1.0f);

        // Get and store camera offset to player
        m_offset = transform.position - m_activePlayer.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Empty
    }

    // Guarenteed to run after Update()
    void LateUpdate()
    {
        // Update camera position after the player's position has definitely 
        // been updated
        transform.position = m_activePlayer.transform.position + m_offset;
    }
}
