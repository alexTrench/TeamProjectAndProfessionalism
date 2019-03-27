using UnityEngine;

public class Player : BaseCharacter
{
    private PlayerController m_playerController;

    // Start is called before the first frame update
    void Start()
    {
        m_playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isDead)
        {
            m_playerController.enabled = false;
            return;
        }
    }
}
