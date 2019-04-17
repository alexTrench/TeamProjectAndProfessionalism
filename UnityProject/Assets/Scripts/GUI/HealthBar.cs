using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // variables which are accesible from inside the editor in order to link scene objects
    [SerializeField] private GameObject   healthBar     = null; // GUI representation of the Health bar
    [SerializeField] private Text         healthText    = null; // UI Text element for the health number
    [SerializeField] private Text         maxHealthText = null; // UI Text element for the Max HP

    // local private health system
    private HealthSystem healthSystem;

    // initialise a bar using a HealthSystem object as parameter
    public void Setup(HealthSystem healthSystem)
    {
        this.healthSystem = healthSystem;

        // OnHealthChanged is an EventHandler and it activates the function below
        healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
    }

    private void HealthSystem_OnHealthChanged(object sender, System.EventArgs e)
    {
        // update the visual representation of the Health Bar,  the Text value of the current health and max health
        healthBar.transform.localScale = new Vector3(healthSystem.GetHealthPercent(), 1);
        healthText.text                = ((int)healthSystem.GetHealth()).ToString();
        maxHealthText.text             = ((int)healthSystem.GetMaxHealth()).ToString();
    }
}
