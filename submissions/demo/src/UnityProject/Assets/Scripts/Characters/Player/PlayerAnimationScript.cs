using UnityEngine;
using UnityEngine.AI;

public class PlayerAnimationScript : MonoBehaviour
{
    private Player       m_player;
    private Animator     m_animator;
    private NavMeshAgent m_nav;
    private bool         m_isDeadTriggerSet = false;

    private readonly int m_directionHash         = Animator.StringToHash("Direction");
    private readonly int m_speedHash             = Animator.StringToHash("Speed");
    private readonly int m_isReloadingHash       = Animator.StringToHash("IsReloading");
    private readonly int m_isDeadHash            = Animator.StringToHash("IsDead");
    private readonly int m_isThrowingGrenadeHash = Animator.StringToHash("IsThrowingGrenade");
    private readonly int m_isRevivedHash         = Animator.StringToHash("IsRevived");

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
        if(!OpenPauseMenu.IsPaused()) {
            // Check if player is dead
            if (m_player.IsDead())
            {
                if (!m_isDeadTriggerSet)
                {
                    m_animator.SetTrigger(m_isDeadHash);
                    m_isDeadTriggerSet = true;
                }
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

    public void Revive()
    {
        m_isDeadTriggerSet = false;
        m_animator.SetTrigger(m_isRevivedHash);
    }
}
