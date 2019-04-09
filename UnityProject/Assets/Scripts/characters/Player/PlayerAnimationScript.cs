using UnityEngine;
using UnityEngine.AI;

public class PlayerAnimationScript : MonoBehaviour
{
    private Player       m_player;
    private Animator     m_animator;
    private NavMeshAgent m_nav;

    private readonly int m_directionHash         = Animator.StringToHash("Direction");
    private readonly int m_speedHash             = Animator.StringToHash("Speed");
    private readonly int m_isReloadingHash       = Animator.StringToHash("IsReloading");
    private readonly int m_isDeadHash            = Animator.StringToHash("IsDead");
    private readonly int m_isThrowingGrenadeHash = Animator.StringToHash("IsThrowingGrenade");

    // Start is called before the first frame update
    void Start()
    {
        m_player = GetComponent<Player>();
        m_animator = GetComponentInChildren<Animator>();
        m_nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if player is dead
        if (m_player.IsDead())
        {
            m_animator.SetTrigger(m_isDeadHash);
            return;
        }

        // Update speed and direction
        float speed = 0.0f;
        float direction = 0.0f;
        if (m_player.IsPlayerControlled()) // Player controlled
        {
            speed = InputManager.GetBackwardAxis();
            direction = InputManager.GetRightAxis();
        }
        else // AI controlled
        {
            speed = m_nav.velocity.magnitude;
        }
        m_animator.SetFloat(m_directionHash, direction);
        m_animator.SetFloat(m_speedHash, speed);

        // Reloading
        m_animator.SetBool("IsReloading", m_player.IsReloading());

        if (InputManager.ThrowGrenade()) {
            // Play throwing grenade animation
            m_animator.SetTrigger(m_isThrowingGrenadeHash);
        }
    }
}
