﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class OpenPauseMenu : MonoBehaviour
{
    // variable which is accesible from inside the editor in order to link scene objects
    [SerializeField] private GameObject pauseMenuUI = null; // visual representation of the pause menu
    [SerializeField] private GameObject firstMenu   = null; // first menu from the pause menu

    private static bool isPaused;
    private bool pressedOnce;

    // Start is called before the first frame update
    void Start()
    {
        isPaused    = false;
        pressedOnce = false;
    }

    // Update is called once per frame
    void Update()
    {
        // update the active status of the pause menu in a local variable
        isPaused = pauseMenuUI.activeSelf;

        // if user has pressed Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // switch between active/inactive
            isPaused = !isPaused;
            pressedOnce = true;
        }
        if (isPaused)
        {
            if(OpenEndGameMenu.IsEndGame() == false)
            {
                ActivateMenu();
            }
        }
        else
        {
            if(firstMenu.GetComponent<CanvasGroup>().alpha == 1 && pressedOnce)
            {
                DeactivateMenu();
            }
        }

        if (!pressedOnce && OpenEndGameMenu.IsEndGame() == false)
        {
            DeactivateMenu();
        }
    }

    void ActivateMenu()
    {
        // pause the time, make pause menu active and set the cursor to active
        Time.timeScale = 0.0f;
        pauseMenuUI.SetActive(true);
        Cursor.visible = true;
    }

    void DeactivateMenu()
    {
        // unpause the time, make pause menu inactive ans set the cursor to inactive
        Time.timeScale = 1.0f;
        pauseMenuUI.SetActive(false);
        Cursor.visible = false;
    }

    public static bool IsPaused() => isPaused;
}
