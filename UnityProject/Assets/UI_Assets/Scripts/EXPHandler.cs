using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EXPHandler : MonoBehaviour
{
    [SerializeField] private EXPBar    expBar;
    [SerializeField] private Text      expValue;
    [SerializeField] private Text      level;
    private EXPSystem expSystem;

    private int currentExpValue;
    private int updatedExpValue;

    private int levelNumber;

    private int remainingEXP;
    private int levelsUnlocked;

    int test = 1;
    

    // Start is called before the first frame update
    void Start()
    {
        expSystem = new EXPSystem(100);
        expBar.Setup(expSystem);

        currentExpValue = int.Parse(expValue.text);
    }

    private void Update()
    {
        updatedExpValue = int.Parse(expValue.text);

        if(updatedExpValue != currentExpValue)
        {
            if(updatedExpValue < 100)
            {
                expValue.text = updatedExpValue.ToString();
                expSystem.IncreaseExp(updatedExpValue);
                currentExpValue = updatedExpValue;
            } else
            {
                levelsUnlocked = updatedExpValue / 100;
                remainingEXP   = updatedExpValue % 100;

                expValue.text = remainingEXP.ToString();
                expSystem.IncreaseExp(remainingEXP);
                currentExpValue = updatedExpValue;

                levelNumber = int.Parse(level.text);
                levelNumber = levelNumber + levelsUnlocked;
                level.text = levelNumber.ToString();
            }
        }
    }
}
