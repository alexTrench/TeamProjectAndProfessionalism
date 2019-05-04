using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPBar : MonoBehaviour
{
    // variable which is accesible from inside the editor in order to link scene objects
    [SerializeField] private GameObject expBar = null; // GUI representation of the EXP bar

    // local private experience system
    private EXPSystem expSystem;

    // initialise a bar using an EXPSystem object as parameter
    public void Setup(EXPSystem expSystem)
    {
        this.expSystem = expSystem;

        // OnEXPChanged is an EventHandler and it activates the function below
        expSystem.OnEXPChanged += EXPSystem_OnEXPChanged;
    }

    private void EXPSystem_OnEXPChanged(object sender, System.EventArgs e)
    {
        // update the visual representation of the bar
        expBar.transform.localScale = new Vector3(expSystem.GetExpPercent(), 1);
    }
}
