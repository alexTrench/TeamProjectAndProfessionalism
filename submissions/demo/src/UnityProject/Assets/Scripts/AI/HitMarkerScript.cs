﻿using UnityEngine;

public class HitMarkerScript : MonoBehaviour
{
    private CapsuleCollider m_capsuleCollider;
    private int m_markerAttackDamage;

    // Start is called before the first frame update
    void Start()
    {
        m_capsuleCollider = GetComponent<CapsuleCollider>();
        DisableMarker();
        m_markerAttackDamage = GetComponentInParent<ZombieAttack>().m_attackDamage;
    }
    
    public void EnableMarker()
    {
        if (m_capsuleCollider) {
            m_capsuleCollider.enabled = true;
        }
    }

    public void DisableMarker()
    {
        if(m_capsuleCollider) {
            m_capsuleCollider.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            other.GetComponent<Player>().TakeDamage(m_markerAttackDamage);
    }
}
