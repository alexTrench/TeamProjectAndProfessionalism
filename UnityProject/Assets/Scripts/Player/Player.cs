using UnityEngine;
using UnityEngine.AI;

public class Player : BaseCharacter
{
    private PlayerController       m_playerController;
    private bool                   m_isPlayerControlled = false;
    private NavMeshAgent           m_nav;
    private CharacterManagerScript m_characterManager;

    // Start is called before the first frame update
    void Start()
    {
        m_playerController = GetComponent<PlayerController>();
        m_nav = GetComponent<NavMeshAgent>();
        m_characterManager = GameObject.FindGameObjectWithTag("CharacterManager").GetComponent<CharacterManagerScript>();
        Debug.Assert(m_characterManager != null, "Could not find CharacterManagerScript!");
    }

    // Update is called once per frame
    void Update()
    {
        // Check if dead
        if (m_isDead)
        {
            m_nav.enabled = false;
            m_playerController.enabled = false;
            return;
        }

        // Move towards player controlled character
        if (!m_isPlayerControlled)
        {
            m_nav.SetDestination(m_characterManager.GetCurrentPlayer().transform.position);
        }
    }

    public bool IsPlayerControlled()
    {
        return m_isPlayerControlled;
    }

    public void SetIsPlayerControlled(bool isPlayerControlled)
    {
        m_isPlayerControlled = isPlayerControlled;
    }
}
