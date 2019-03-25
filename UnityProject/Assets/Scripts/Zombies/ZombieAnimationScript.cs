using UnityEngine;

public class ZombieAnimationScript : MonoBehaviour
{
    private Zombie m_zombie;
    private Animator m_animator;

    private readonly int m_isDeadHash = Animator.StringToHash("IsDead");

    // Start is called before the first frame update
    void Start()
    {
        m_zombie = GetComponent<Zombie>();
        m_animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_zombie.IsDead())
        {
            m_animator.SetTrigger(m_isDeadHash);
            return;
        }


    }
}
