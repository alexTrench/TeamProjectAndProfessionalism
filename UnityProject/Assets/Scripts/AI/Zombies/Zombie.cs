using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : BaseCharacter
{
    private CharacterManagerScript m_characterManager;
    private List<Player>           m_playerCharacters;
    private Transform              m_targetTransform;
    private NavMeshAgent           m_nav;
    private float                  m_timer;

    public float m_secsBeforePlayerSearch = 5.0f;
    public float m_secsBeforeDestroy      = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Init character manager
        m_characterManager = GameObject.FindGameObjectWithTag("CharacterManager").GetComponent<CharacterManagerScript>();
        Debug.Assert(m_characterManager != null, "Could not find CharacterManagerScript component!");

        // Get and store all player characters
        m_playerCharacters = m_characterManager.GetPlayerCharacters();

        // Init nav agent
        m_nav = GetComponent<NavMeshAgent>();

        // Init timer
        m_timer = m_secsBeforePlayerSearch;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if dead
        if (m_isDead)
        {
            m_nav.enabled = false;
            Destroy(gameObject, m_secsBeforeDestroy);
            return;
        }

        // Update player character target
        if (m_timer >= m_secsBeforePlayerSearch)
        {
            m_timer = 0.0f;
            m_targetTransform = GetNearestPlayer().transform;
        }
        m_timer += Time.deltaTime;

        // Look at and move to target transform
        transform.LookAt(m_targetTransform);
        m_nav.SetDestination(m_targetTransform.position);
    }

    private Player GetNearestPlayer()
    {
        // Find nearest player character
        float distance = float.MaxValue;
        int index = 0;

        for (int i = 0; i < m_playerCharacters.Count; i++)
        {
            if (!m_playerCharacters[i].IsDead())
            {
                float distanceToPlayer = (this.transform.position - m_playerCharacters[i].transform.position).magnitude;
                if (distanceToPlayer < distance)
                {
                    distance = distanceToPlayer;
                    index = i;
                }
            }
        }

        return m_playerCharacters[index];
    }
}
