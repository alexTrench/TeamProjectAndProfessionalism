using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientMusic : MonoBehaviour
{
    private AudioSource m_audioSource = null;
    private bool        m_isResumed   = false;

    // Start is called before the first frame update
    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (OpenPauseMenu.IsPaused() || OpenEndGameMenu.IsEndGame())
        {
            m_audioSource.Pause();
        }
        else
        {
            m_isResumed = true;
        }

        if (m_isResumed == true)
        {
            m_audioSource.UnPause();
            m_isResumed = false;
        }
    }
}
