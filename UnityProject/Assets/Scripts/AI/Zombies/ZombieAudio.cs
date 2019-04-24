using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAudio : MonoBehaviour
{
    [SerializeField] private AudioClip[] m_audioClipArray;
    [SerializeField] private AudioSource m_audioSource;

    private int m_numSoundsPlaying = 0;
    private bool m_startTimer = false;
    private float m_timer = 0.0f;
    private int m_maxSounds = 5;

    public void PlaySound(Vector3 position)
    {
        if (m_startTimer)
            m_timer += Time.deltaTime;

        if (m_timer >= 3.0f)
        {
            m_timer = 0.0f;
            m_numSoundsPlaying--;
        }

        if (m_numSoundsPlaying > m_maxSounds)
            return;

        int index = Random.Range(0, m_audioClipArray.Length);
        AudioSource.PlayClipAtPoint(m_audioClipArray[index], position);
        m_numSoundsPlaying++;
        
            m_startTimer = (m_numSoundsPlaying > m_maxSounds) ? true : false;
    }
}
