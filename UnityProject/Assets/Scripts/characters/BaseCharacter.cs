using UnityEngine;

public class BaseCharacter : MonoBehaviour, IDamagable
{
    [SerializeField] protected float m_health    = 100.0f;
    [SerializeField] protected float m_maxHealth = 100.0f;
    // add energy and max energy for each character.

    protected bool m_isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        // Empty   
    }

    // Update is called once per frame
    void Update()
    {
        // Empty
    }

    // Take damage interface
    public void TakeDamage(float damage)
    {
        if (!m_isDead)
        {
            m_health -= damage;
            if (m_health <= 0.0f)
                m_isDead = true;
        }
    }

    // Returns true if the character is dead
    public bool IsDead()
    {
        return m_isDead;
    }

    // Returns the current health of the character
    public float GetHealth()
    {
        return m_health;
    }

    // Returns the max health of the character
    public float GetMaxHealth()
    {
        return m_maxHealth;
    }
}
