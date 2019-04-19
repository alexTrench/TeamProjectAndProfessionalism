using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    public int m_attackDamage = 10;

    private readonly int m_isAttackingHash = Animator.StringToHash("IsAttacking");

    private Animator m_animator;
    private Zombie m_zombie;
    private bool m_playerInRange = false;

    private void Awake()
    {
        m_animator = GetComponentInChildren<Animator>();
        m_zombie = GetComponent<Zombie>();
    }
    
    // Update is called once per frame
    void Update()
    {
        // If zombie is dead, just return
        if (m_zombie.IsDead()) return;
        
        // If a player is in range, then attack
        if (m_playerInRange) Attack();
    }

    /**
     * @brief Applys a modifier to the zombie's attack.
     * @param attackDamage - The modifier to be applied.
     */
    public void ApplyAttackDamageModifier(float attackDamage) {
        m_attackDamage += (int)(attackDamage * m_attackDamage);
    }

    void Attack()
    {
        m_animator.SetTrigger(m_isAttackingHash);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && 
            !other.gameObject.GetComponent<Player>().IsDead())
        {
            m_playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            m_playerInRange = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            m_playerInRange = !other.gameObject.GetComponent<Player>().IsDead();
    }
}
