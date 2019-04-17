using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    public int m_attackDamage = 10;
    public float m_timeBetweenAttacks = 0.5f;

    private readonly int m_isAttackingHash = Animator.StringToHash("IsAttacking");

    private Animator m_animator;
    private GameObject m_playerObject;
    private Player m_player;
    private bool m_playerInRange = false;
    private float m_attackTimer = 0.0f;

    private void Awake()
    {
        m_animator = GetComponentInChildren<Animator>();
        m_playerObject = GameObject.FindGameObjectWithTag("Player");
        m_player = m_playerObject.GetComponent<Player>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Empty
    }

    // Update is called once per frame
    void Update()
    {
        m_attackTimer += Time.deltaTime;

        if (m_attackTimer >= m_timeBetweenAttacks && m_playerInRange)
            Attack();
    }

    /**
     * @brief Applys a modifier to the zombie's attack.
     * @param attackDamage - The modifier to be applied.
     */
    public void ApplyAttackDamageModifier(float attackDamage) {
        m_attackDamage *= (int)attackDamage;
    }

    void Attack()
    {
        m_attackTimer = 0.0f;

        if (!m_player.IsDead())
            m_animator.SetTrigger(m_isAttackingHash);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == m_playerObject)
            m_playerInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == m_playerObject)
            m_playerInRange = false;
    }
}
