using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private HealthSystem healthSystem;
    public  GameObject   healthBar;
    public  Text         healthText;
    public  Text         maxHealthText;

    public void Setup(HealthSystem healthSystem)
    {
        this.healthSystem = healthSystem;

        healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
    }

    private void HealthSystem_OnHealthChanged(object sender, System.EventArgs e)
    {
        healthBar.transform.localScale = new Vector3(healthSystem.GetHealthPercent(), 1);
        healthText.text = ((int)healthSystem.GetHealth()).ToString();
        //maxHealthText.text = ((int)healthSystem.GetHealth()).ToString();
    }
}
