using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PressOToIncreaseEXP : MonoBehaviour
{
    // variable which is accesible from inside the editor in order to link scene objects
    [SerializeField] private Text expText = null; // UI Text element for the EXP value

    // local private value for the EXP
    private int expValue;

    // Start is called before the first frame update
    private void Start()
    {
        // initialise the local value
        expValue = int.Parse(expText.text);
    }

    // Update is called once per frame
    private void Update()
    {
        // update the EXP value
        expValue = int.Parse(expText.text);

        // if user pressed 'O'
        if (Input.GetKeyDown(KeyCode.O))
        {
            // activate the cheat and increase EXP by 5 points
            expValue = expValue + 5;
            expText.text = expValue.ToString();
        }
    }
}
