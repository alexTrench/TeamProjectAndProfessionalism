using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EXPHandler : MonoBehaviour
{
    // variables which are accesible from inside the editor in order to link scene objects
    [SerializeField] private EXPBar expBar     = null; // GUI representation of EXP bar
    [SerializeField] private Text   expValue   = null; // UI Text that holds the EXP value
    [SerializeField] private Text   level      = null; // UI Text element for the level of the user
    [SerializeField] private Text   maxExpText = null; // UI Text element for the max EXP needed to be achieved for level up
    
    // local private EXP system
    private EXPSystem expSystem;

    // local private current and updated version of EXP value
    private int currentExpValue;
    private int updatedExpValue;

    // local private level value
    private int levelNumber;

    // local private value of EXP and remaining EXP after level UP
    private int maxEXP;
    private int remainingEXP;

    // Start is called before the first frame update
    void Start()
    {
        // initialise exp and the system that controls the logic
        // and also setup the bar using the EXP system. Update the max EXP text as well
        maxEXP = 100;
        expSystem = new EXPSystem(maxEXP);
        expBar.Setup(expSystem);
        maxExpText.text = maxEXP.ToString();

        // initialise the current exp value (= 0)
        currentExpValue = int.Parse(expValue.text);
    }

    private void Update()
    {
        // update each frame the exp value
        updatedExpValue = int.Parse(expValue.text);

        // if the EXP value has changed 
        if (updatedExpValue != currentExpValue)
        {
            // if the EXP value is below the max require for level up
            if(updatedExpValue < maxEXP)
            {
                // update the text
                expValue.text = updatedExpValue.ToString();

                // increase EXP using the system 
                expSystem.IncreaseExp(updatedExpValue);
                currentExpValue = updatedExpValue;
            } else
            {
                // otherwise, save the remaining exp into a local variable
                remainingEXP   = updatedExpValue % maxEXP;

                // update the UI Text for EXP
                expValue.text = remainingEXP.ToString();

                // update the EXP system as well
                expSystem.IncreaseExp(remainingEXP);
                currentExpValue = remainingEXP;

                // check the current level and increase it by 1
                levelNumber = int.Parse(level.text);
                levelNumber++;
                level.text = levelNumber.ToString();

                // also increase the max EXP for next level by 10
                maxEXP = maxEXP + 10;
                maxExpText.text = maxEXP.ToString();
                expSystem.SetMaxExp(maxEXP);
            }
        }
    }
}
