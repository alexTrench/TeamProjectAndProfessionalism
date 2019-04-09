using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EXPHandler : MonoBehaviour
{
    [SerializeField] private EXPBar    expBar;
    [SerializeField] private Text      expValue;
    [SerializeField] private Text      level;
    [SerializeField] private Text      maxExpText;
    private EXPSystem expSystem;

    private int currentExpValue;
    private int updatedExpValue;

    private int levelNumber;

    private int maxEXP;
    private int remainingEXP;

    // Start is called before the first frame update
    void Start()
    {
        maxEXP = 100;
        expSystem = new EXPSystem(maxEXP);
        expBar.Setup(expSystem);
        maxExpText.text = maxEXP.ToString();

        currentExpValue = int.Parse(expValue.text);
    }

    private void Update()
    {
        updatedExpValue = int.Parse(expValue.text);

        if (updatedExpValue != currentExpValue)
        {
            if(updatedExpValue < maxEXP)
            {
                expValue.text = updatedExpValue.ToString();

                expSystem.IncreaseExp(updatedExpValue);
                currentExpValue = updatedExpValue;
            } else
            {
                remainingEXP   = updatedExpValue % maxEXP;

                expValue.text = remainingEXP.ToString();

                expSystem.IncreaseExp(remainingEXP);
                currentExpValue = remainingEXP;

                levelNumber = int.Parse(level.text);
                levelNumber++;
                level.text = levelNumber.ToString();

                maxEXP = maxEXP + 10;
                maxExpText.text = maxEXP.ToString();
                expSystem.SetMaxExp(maxEXP);
            }
        }
    }
}
