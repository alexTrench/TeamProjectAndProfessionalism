﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrower : MonoBehaviour
{

    public float throwForce = 40f;
    public GameObject grenade;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            ThrowGrenade();
        }
    }

    void ThrowGrenade()
    {
        if (gameObject != null && gameObject.activeInHierarchy) {
            GameObject grenadeCopy = Instantiate(
                grenade, 
                transform.position, 
                transform.rotation,
                gameObject.transform
            );

            Rigidbody rb = grenadeCopy.GetComponent<Rigidbody>();

            rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
        }
    }
}
