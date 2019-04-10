using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPBar : MonoBehaviour
{
    private EXPSystem expSystem;
    [SerializeField] private GameObject expBar;

    public void Setup(EXPSystem expSystem)
    {
        this.expSystem = expSystem;

        expSystem.OnEXPChanged += EXPSystem_OnEXPChanged;
    }

    private void EXPSystem_OnEXPChanged(object sender, System.EventArgs e)
    {
        expBar.transform.localScale = new Vector3(expSystem.GetExpPercent(), 1);
    }
}
