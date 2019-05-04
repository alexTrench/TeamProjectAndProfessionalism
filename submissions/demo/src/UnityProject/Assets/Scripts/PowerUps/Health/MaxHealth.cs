using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHealth : MonoBehaviour
{

    public GameObject pickupEffect;
    private float currentMaxHealth;

    private void Start()
    {
      
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            currentMaxHealth = other.GetComponent<Player>().GetMaxHealth();
            other.GetComponent<Player>().SetHealth(currentMaxHealth);

            Instantiate(pickupEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

}
