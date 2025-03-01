﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;

public class OpenEndGameMenu : MonoBehaviour
{
    // variable which is accesible from inside the editor in order to link scene objects
    [SerializeField] private GameObject endGameUI = null; // visual representation of the pause menu
    [SerializeField] private Button retryButton = null;

    private static bool isEndGame;
    private int numberOfDead;

    // characters manager
    private CharacterManagerScript charactersManager;

    // Start is called before the first frame update
    void Start()
    {
        retryButton.onClick.AddListener(terminateEndGame);
        // look on the list of objects and get the component for character manager script
        charactersManager = GameObject.FindGameObjectWithTag("CharacterManager").GetComponent<CharacterManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(charactersManager.AreAllPlayersDead())
        {
            ActivateEndMenu();
        }
        else
        {
            terminateEndGame();
        }
    }

    void ActivateEndMenu()
    {
        // pause the time, make pause menu active and set the cursor to active
        isEndGame = true;
        Time.timeScale = 0.0f;
        Debug.Log("timescale is: " + Time.timeScale);
        Cursor.visible = true;
        endGameUI.SetActive(true);
    }

    void terminateEndGame()
    {
        isEndGame = false;
        //Time.timeScale = 1.0f;
        //Cursor.visible = false;
        //endGameUI.SetActive(false);
    }

    public static bool IsEndGame() => isEndGame;
}
