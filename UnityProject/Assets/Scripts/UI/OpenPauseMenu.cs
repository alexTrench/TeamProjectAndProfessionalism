using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class OpenPauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI = null;

    private static bool isPaused;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        isPaused = pauseMenuUI.activeSelf;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }

        if (isPaused)
        {
            ActivateMenu();
        }
        else
        {
            DeactivateMenu();
        }
    }

    void ActivateMenu()
    {
        Time.timeScale = 0.0f;
        pauseMenuUI.SetActive(true);
        Cursor.visible = true;
    }

    void DeactivateMenu()
    {
        Time.timeScale = 1.0f;
        pauseMenuUI.SetActive(false);
        Cursor.visible = false;
    }

    public static bool IsPaused() => isPaused;
}
