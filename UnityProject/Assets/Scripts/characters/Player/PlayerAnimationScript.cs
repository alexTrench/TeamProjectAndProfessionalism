using UnityEngine;

public class PlayerAnimationScript : MonoBehaviour
{
    private Player m_player;
    private Animator m_animator;

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

        float speed = InputManager.GetBackwardAxis();
        float direction = InputManager.GetRightAxis();

        // Update direction
        m_animator.SetFloat(m_directionHash, direction);

        // Update speed
        m_animator.SetFloat(m_speedHash, speed);

        if (InputManager.Reload()) {
            // Play reload animation
            m_animator.SetTrigger(m_isReloadingHash);
        }
        if (InputManager.ThrowGrenade()) {
            // Play throwing grenade animation
            m_animator.SetTrigger(m_isThrowingGrenadeHash);
        }
        if (InputManager.Die()) {
            m_animator.SetTrigger(m_isDeadHash);
        }
    }
}
