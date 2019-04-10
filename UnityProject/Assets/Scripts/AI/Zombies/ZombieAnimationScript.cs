using UnityEngine;
using UnityEngine.AI;

public class ZombieAnimationScript : MonoBehaviour
{
    private CharacterManagerScript m_characterManager;
    private Zombie                 m_zombie;
    private Animator               m_animator;
    private NavMeshAgent           m_nav;
    
    private readonly int m_isDeadHash = Animator.StringToHash("IsDead");
    private readonly int m_speedHash = Animator.StringToHash("Speed");
    
    // Start is called before the first frame update
    void Start()
    {
        // Init private variables
        m_characterManager = GameObject.FindGameObjectWithTag("CharacterManager").GetComponent<CharacterManagerScript>();
        Debug.Assert(m_characterManager != null, "Could not find CharacterManagerScript component!");
        m_zombie = GetComponent<Zombie>();
        m_animator = GetComponentInChildren<Animator>();
        m_nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if zombie is dead
        if (m_zombie.IsDead())
        {
            m_animator.SetTrigger(m_isDeadHash);
            return;
        }

        // Check if all players are dead (MOVE THIS INTO MANAGER TO PREVENT MULTIPLE CALLS!!!)
        if (m_characterManager.AreAllPlayersDead())
        {
            m_animator.SetFloat(m_speedHash, 0.0f);
            return;
        }

        // Update movement speed
        float currentSpeed = m_nav.velocity.magnitude;
        m_animator.SetFloat(m_speedHash, currentSpeed);

        if (Input.GetKeyDown(KeyCode.N))
            m_nav.speed = 0.5f;
        else if (Input.GetKeyDown(KeyCode.M))
            m_nav.speed = 2.0f;
    }
}
