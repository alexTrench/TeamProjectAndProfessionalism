using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationScript : MonoBehaviour
{
    private Animator m_animator;

    private readonly int m_directionHash         = Animator.StringToHash("Direction");
    private readonly int m_speedHash             = Animator.StringToHash("Speed");
    private readonly int m_isReloadingHash       = Animator.StringToHash("IsReloading");
    private readonly int m_isDeadHash            = Animator.StringToHash("IsDead");
    private readonly int m_isThrowingGrenadeHash = Animator.StringToHash("IsThrowingGrenade");

    // Start is called before the first frame update
    void Start()
    {
        m_animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float direction = Input.GetAxis("Horizontal");
        float speed = Input.GetAxis("Vertical");

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
