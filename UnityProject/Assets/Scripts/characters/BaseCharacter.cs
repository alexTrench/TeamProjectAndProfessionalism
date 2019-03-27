using UnityEngine;

public class BaseCharacter : MonoBehaviour, IDamagable
{
    public int m_health = 100;
    public int m_maxHealth = 100;

    protected bool m_isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        if (!m_isDead)
        {
            m_health -= damage;
            if (m_health <= 0)
                m_isDead = true;
        }
    }

    public bool IsDead()
    {
        return m_isDead;
    }
}
