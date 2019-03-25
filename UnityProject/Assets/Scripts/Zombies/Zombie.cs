using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : BaseCharacter
{
    private Transform m_playerTransform;
    private NavMeshAgent m_nav;

    public float m_secondsBeforeDestroy = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        m_nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isDead)
        {
            m_nav.enabled = false;
            Destroy(gameObject, m_secondsBeforeDestroy);
            return;
        }

        m_nav.SetDestination(m_playerTransform.position);
    }
}
